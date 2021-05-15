using AutoMapper;

using DavidMorales.Domain.Authorization;
using DavidMorales.Domain.Interfaces.Services;
using DavidMorales.Services.Api.Helpers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace DavidMorales.Services.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class LogDataChangeController : Controller
    {
        private readonly ILogDataChangeService _logDataChangeService;
        private readonly IMapper _mapper;


        public LogDataChangeController(
            ILogDataChangeService logDataChangeService,
            IMapper mapper)
        {
            _logDataChangeService = logDataChangeService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = AppPermissions.LogDataChange.Query)]
        public async Task<IActionResult> Get()
        {
            var people = await _logDataChangeService.GetAsync();
            return ResponseHelper.Ok(people);
        }

    }
}
