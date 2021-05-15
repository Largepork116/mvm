using DavidMorales.Domain.Entities;
using DavidMorales.Domain.Exceptions;
using DavidMorales.Domain.Interfaces;
using DavidMorales.Domain.Interfaces.Services;

using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidMorales.Services.AppServices
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PersonService> _logger;

        public PersonService(
            IUnitOfWork unitOfWork,
            ILogger<PersonService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Person>> GetAsync()
        {
            var people = await _unitOfWork.People.GetAsync(
                null, 
                o => o.OrderBy(x=> x.Name),
                i => i.Company,
                i => i.User);
            return people;
        }

        public async Task<IEnumerable<Person>> GetExternalAsync()
        {
            var people = await _unitOfWork.People.GetAsync(
                x=> x.User == null,
                o => o.OrderBy(x => x.Name),
                i => i.Company,
                i => i.User);
            return people;
        }

        public async Task<IEnumerable<Person>> GetInternalAsync()
        {
            var people = await _unitOfWork.People.GetAsync(
                x => x.User != null,
                o => o.OrderBy(x => x.Name),
                i => i.Company,
                i => i.User);
            return people;
        }

        public async Task<Person> FindAsync(int personId)
        {
            var category = await _unitOfWork.People
                .FindAsync(personId);

            if (category == null)
            {
                throw new AppNotFoundException("No se encontró la persona solicitada");
            }

            return category;
        }

        public async Task CreateAsync(Person person)
        {
            await _unitOfWork.People.AddAsync(person);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(int personId, Person edited)
        {
            var person = await _unitOfWork.People
                .GetSingleAsync(x => x.PersonId == personId);

            if (person == null)
            {
                throw new AppNotFoundException("No se encontró la persona solicitada");
            }

            _unitOfWork.People.Update(person, edited);
            _unitOfWork.SaveChanges();
        }
    }
}
