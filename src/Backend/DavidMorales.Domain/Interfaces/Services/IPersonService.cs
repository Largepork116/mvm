using DavidMorales.Domain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DavidMorales.Domain.Interfaces.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAsync();
        Task<IEnumerable<Person>> GetExternalAsync();
        Task<IEnumerable<Person>> GetInternalAsync();
        Task<Person> FindAsync(int personId);
        Task CreateAsync(Person person);
        Task UpdateAsync(int personId, Person edited);
    }
}
