using DavidMorales.Domain.Entities;
using DavidMorales.Domain.Interfaces.Repositories;
using DavidMorales.Infrastructure.Data.Repositories.Base;

using Microsoft.EntityFrameworkCore;

namespace DavidMorales.Infrastructure.Data.Repositories
{
    public class LogDataChangeRepository : Repository<LogDataChange>, ILogDataChangeRepository
    {
        public LogDataChangeRepository(DbContext context) : base(context)
        {

        }
    }
}
