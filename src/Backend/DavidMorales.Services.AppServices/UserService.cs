using DavidMorales.Domain.Authorization;
using DavidMorales.Domain.Entities;
using DavidMorales.Domain.Exceptions;
using DavidMorales.Domain.Interfaces;
using DavidMorales.Domain.Interfaces.Services;
using DavidMorales.Domain.Security.Authentication;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DavidMorales.Services.AppServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppIdentity _appIdentity;

        public UserService(
            IUnitOfWork unitOfWork,
            ILogger<UserService> logger,
            AppIdentity appIdentity,
            UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userManager = userManager;
            _appIdentity = appIdentity;
        }

        public async Task CreateAsync(string email, string rolName, string pasword, Person person)
        {
            var rol = AppRoles.FindByName(rolName);

            var exist = await _userManager.FindByEmailAsync(email);

            if(exist != null)
            {
                throw new AppException("El usuario ya existe");
            }

            var user = new AppUser
            {
                Email = email,
                UserName = email,
                Person = person
            };

            var result = await _userManager.CreateAsync(user, pasword);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, rol.Name);
            }
        }

        public async Task UpdateAsync(string email, string rolName)
        {
            var rol = AppRoles.FindByName(rolName);

            var edit = await _userManager.FindByEmailAsync(email);

            if(edit == null)
            {
                throw new AppNotFoundException("Usuario no encontrado");
            }

            var roles = await _userManager.GetRolesAsync(edit);
            await _userManager.RemoveFromRolesAsync(edit, roles);
            await _userManager.AddToRoleAsync(edit, rol.Name);

            var log = new LogDataChange
            {
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = _appIdentity.Username,
                Table = "UsersRoles",
                Pk = int.Parse(edit.Id.ToString()),
                Changes = $"Role: {roles[0]} => {rol.Name}"
            };

            await _unitOfWork.LogsDataChanges.AddAsync(log);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetUserWithRoleAsync()
        {
            return await _unitOfWork.Users.GetUserWithRoleAsync();
        }

    }
}
