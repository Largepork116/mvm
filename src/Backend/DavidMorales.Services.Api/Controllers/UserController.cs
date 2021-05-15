using AutoMapper;

using DavidMorales.Domain.Authorization;
using DavidMorales.Domain.Entities;
using DavidMorales.Domain.Interfaces.Services;
using DavidMorales.Services.Api.Helpers;
using DavidMorales.Services.Api.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace DavidMorales.Services.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = AppPermissions.User.Query)]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetUserWithRoleAsync();

            return ResponseHelper.Ok(users);
        }

        [HttpGet("roles")]
        [Authorize(Policy = AppPermissions.User.Query)]
        public async Task<IActionResult> GetRoles()
        {
            await Task.CompletedTask;
            return ResponseHelper.Ok(AppRoles.Get());
        }


        [HttpPost]
        [Authorize(Policy = AppPermissions.User.Add)]
        public async Task<IActionResult> Create([FromBody] UserCreateViewModel userViewModel)
        {
            var person = _mapper.Map<Person>(userViewModel.Person);

            await _userService.CreateAsync(userViewModel.Email,  userViewModel.Role, userViewModel.Password, person);
            return ResponseHelper.Ok(userViewModel);
        }

        [HttpPut]
        [Authorize(Policy = AppPermissions.User.Edit)]
        public async Task<IActionResult> Update([FromBody] UserUpdateViewModel userViewModel)
        {
            await _userService.UpdateAsync(userViewModel.Email, userViewModel.Role); ;
            return ResponseHelper.Ok(userViewModel);
        }

    }
}
