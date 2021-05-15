using DavidMorales.Domain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DavidMorales.Domain.Interfaces.Services
{
    public interface ILogDataChangeService
    {
        Task<IEnumerable<LogDataChange>> GetAsync();
    }
}
