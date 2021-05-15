
using DavidMorales.Domain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DavidMorales.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<object>> GetUserWithRoleAsync();
        Task CreateAsync(string email, string rolName, string pasword, Person person);
        Task UpdateAsync(string email, string rolName);
    }
}
