using DavidMorales.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DavidMorales.Domain.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<string> CrearTokenAsync(string username, Claim[] userClaims, DateTime date, TimeSpan validDate);
        Task<AppUser> LoginAsync(string username, string password);
        Task<List<string>> GetRolesAsync(AppUser user);
        Task<Claim[]> GetRolesClaimsAsync(List<string> roles);
    }
}
