using DavidMorales.Domain.Interfaces.Services;
using DavidMorales.Services.Api.Helpers;
using DavidMorales.Services.Api.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DavidMorales.Services.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthenticationController : Controller
    {

        private readonly IAuthenticationService _service;
        private readonly ILogger _logger;

        public AuthenticationController(
            IAuthenticationService service,
            ILogger<AuthenticationController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Token([FromBody] CredencialesViewModel credenciales)
        {
            var user = await _service.LoginAsync(credenciales.Username, credenciales.Password);

            // Get the roles of the user
            var roles = await _service.GetRolesAsync(user);

            // Get the claims of the user
            var userRolesClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToArray();
            var roleClaims = await _service.GetRolesClaimsAsync(roles);
            var userClaims = new Claim[]
            {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("Username", user.UserName)
            };

            var claims = new Claim[] { }
                    .Union(userClaims)
                    .Union(userRolesClaims)
                    .Union(roleClaims)
                    .ToArray();

            // Create the token
            var date = DateTime.Now;
            var expireDate = TimeSpan.FromDays(1);

            var token = await _service.CrearTokenAsync(user.UserName, claims, date, expireDate);


            // Create the response
            return ResponseHelper.Ok(new
            {
                Token = token,
                Email = user.Email,
                Permissions = roleClaims.Select(x => x.Value),
                Role = userRolesClaims.SingleOrDefault().Value,
                ExpireAt = date.Add(expireDate)
            });
        }
    }
}
