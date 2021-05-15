using DavidMorales.Domain.Entities;
using DavidMorales.Domain.Interfaces;
using DavidMorales.Domain.Interfaces.Services;

using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DavidMorales.Services.AppServices
{
    public class LogDataChangeService : ILogDataChangeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LogDataChangeService> _logger;

        public LogDataChangeService(
            IUnitOfWork unitOfWork,
            ILogger<LogDataChangeService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<LogDataChange>> GetAsync()
        {
            var logs = await _unitOfWork.LogsDataChanges.GetAsync();
            return logs;
        }
    }
}
