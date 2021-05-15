using DavidMorales.Domain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DavidMorales.Domain.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAsync();
        Task CreateAsync(Company company);
        Task UpdateAsync(int companyId, Company edited);
        Task<Company> FindAsync(int companyId);
    }
}
