using DavidMorales.Domain.Entities;
using DavidMorales.Domain.Interfaces.Repositories.Base;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DavidMorales.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<AppUser>
    {
        Task<IEnumerable<object>> GetUserWithRoleAsync();
    }
}
