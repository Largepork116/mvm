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
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;


        public PersonController(
            IPersonService personService,
            IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = AppPermissions.Person.Query)]
        public async Task<IActionResult> Get()
        {
            var people = await _personService.GetAsync();
            return ResponseHelper.Ok(people);
        }

        [HttpGet("internal")]
        [Authorize(Policy = AppPermissions.Person.Query)]
        public async Task<IActionResult> GetInternal()
        {
            var people = await _personService.GetInternalAsync();
            return ResponseHelper.Ok(people);
        }

        [HttpGet("external")]
        [Authorize(Policy = AppPermissions.Person.Query)]
        public async Task<IActionResult> GetExternal()
        {
            var people = await _personService.GetExternalAsync();
            return ResponseHelper.Ok(people);
        }

        [HttpGet("{personId}")]
        [Authorize(Policy = AppPermissions.Person.Query)]
        public async Task<IActionResult> GetById(int personId)
        {
            var person = await _personService.FindAsync(personId);
            return ResponseHelper.Ok(person);
        }

        [HttpPost]
        [Authorize(Policy = AppPermissions.Person.Add)]
        public async Task<IActionResult> Create([FromBody] PersonCreateViewModel personViewModel)
        {
            var person = _mapper.Map<Person>(personViewModel);
            await _personService.CreateAsync(person); ;
            return ResponseHelper.Ok(person);
        }

        [HttpPut]
        [Authorize(Policy = AppPermissions.Person.Edit)]
        public async Task<IActionResult> Put([FromBody] PersonUpdateViewModel personViewModel)
        {
            var person = _mapper.Map<Person>(personViewModel);
            await _personService.UpdateAsync(person.PersonId, person);

            return ResponseHelper.Ok(person);
        }

    }
}
