using DavidMorales.Domain.Entities;
using DavidMorales.Domain.Interfaces.Repositories;
using DavidMorales.Infrastructure.Data.Repositories.Base;

using Microsoft.EntityFrameworkCore;

namespace DavidMorales.Infrastructure.Data.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(DbContext context) : base(context)
        {

        }
    }
}
