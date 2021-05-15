using DavidMorales.Services.Api.Helpers;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace DavidMorales.Services.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TestController : Controller
    {
        public TestController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return ResponseHelper.Ok("OK");
        }

    }
}
