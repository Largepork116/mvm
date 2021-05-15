using DavidMorales.Domain.Authorization;
using DavidMorales.Domain.Entities;
using DavidMorales.Domain.Exceptions;
using DavidMorales.Domain.Interfaces;
using DavidMorales.Domain.Interfaces.Services;
using DavidMorales.Domain.Security.Settings;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DavidMorales.Services.AppServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationSettings _autenticacionSettings;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;


        public AuthenticationService(
            IOptions<AuthenticationSettings> autenticacionSettings,
            IUnitOfWork unitOfWork,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            _autenticacionSettings = autenticacionSettings.Value;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> CrearTokenAsync(string username, Claim[] userClaims, DateTime date, TimeSpan validDate)
        {
            var expire = date.Add(validDate);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                          new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds().ToString(),
                          ClaimValueTypes.Integer64)
            }.Union(userClaims);

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_autenticacionSettings.SigninKey)),
                SecurityAlgorithms.HmacSha256Signature);


            var jwt = new JwtSecurityToken(
                issuer: _autenticacionSettings.Issuer,
                audience: _autenticacionSettings.Audience,
                claims: claims,
                notBefore: date,
                expires: expire,
                signingCredentials: signingCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return await Task.FromResult<string>(encodedJwt);
        }

        public async Task<List<string>> GetRolesAsync(AppUser usuario)
        {
            var roles = await _userManager.GetRolesAsync(usuario);
            return roles.ToList();
        }

        public async Task<Claim[]> GetRolesClaimsAsync(List<string> roles)
        {
            var claims = new List<Claim>();
            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                var claimsOfRoles = await _roleManager.GetClaimsAsync(role);
                claims.AddRange(claimsOfRoles);
            }

            var claimsRoles = claims.Select(r => new Claim(AppClaimTypes.Permission, r.Value)).ToArray();
            return claimsRoles;
        }

        public async Task<AppUser> LoginAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new AppAuthException("Username y/o password no pueden ser vacio");

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(username);

            if (userToVerify == null)
                throw new AppAuthException("Username y/o password no válido");

            // If the credentials are invalid
            var response = await _userManager.CheckPasswordAsync(userToVerify, password);
            if (!await _userManager.CheckPasswordAsync(userToVerify, password))
                throw new AppAuthException("Username y/o password no válido");

            return await Task.FromResult(userToVerify);
        }
    }
}
