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
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;


        public CompanyController(
            ICompanyService companyService,
            IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = AppPermissions.Company.Query)]
        public async Task<IActionResult> Get()
        {
            var companies = await _companyService.GetAsync();
            return ResponseHelper.Ok(companies);
        }

        [HttpGet("{companyId}")]
        [Authorize(Policy = AppPermissions.Company.Query)]
        public async Task<IActionResult> GetById(int companyId)
        {
            var company = await _companyService.FindAsync(companyId);
            return ResponseHelper.Ok(company);
        }

        [HttpPost]
        [Authorize(Policy = AppPermissions.Company.Add)]
        public async Task<IActionResult> Create([FromBody] CompanyCreateViewModel companyViewModel)
        {
            var company = _mapper.Map<Company>(companyViewModel);
            await _companyService.CreateAsync(company); ;
            return ResponseHelper.Ok(company);
        }

        [HttpPut]
        [Authorize(Policy = AppPermissions.Company.Edit)]
        public async Task<IActionResult> Put([FromBody] CompanyUpdateViewModel campaignViewModel)
        {
            var company = _mapper.Map<Company>(campaignViewModel);
            await _companyService.UpdateAsync(company.CompanyId, company);

            return ResponseHelper.Ok(company);
        }
    }
}
