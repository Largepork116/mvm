using DavidMorales.Domain.Entities;
using DavidMorales.Domain.Interfaces.Repositories;
using DavidMorales.Infrastructure.Data.Repositories.Base;

using Microsoft.EntityFrameworkCore;

namespace DavidMorales.Infrastructure.Data.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext context) : base(context)
        {

        }
    }
}
