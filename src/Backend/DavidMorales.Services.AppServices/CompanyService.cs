using DavidMorales.Domain.Entities;
using DavidMorales.Domain.Exceptions;
using DavidMorales.Domain.Interfaces;
using DavidMorales.Domain.Interfaces.Services;

using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DavidMorales.Services.AppServices
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(
            IUnitOfWork unitOfWork,
            ILogger<CompanyService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Company>> GetAsync()
        {
            var companies = await _unitOfWork.Companies.GetAsync();
            return companies;
        }

        public async Task<Company> FindAsync(int companyId)
        {
            var category = await _unitOfWork.Companies
                .FindAsync(companyId);

            if (category == null)
            {
                throw new AppNotFoundException("No se encontró la empresa solicitada");
            }

            return category;
        }

        public async Task CreateAsync(Company company)
        {
            var exists = await _unitOfWork.Companies.GetSingleAsync(x => x.Name == company.Name);

            if (exists != null)
            {
                throw new AppException("La empresa ya existe");
            }

            await _unitOfWork.Companies.AddAsync(company);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(int companyId, Company edited)
        {
            var companies = await _unitOfWork.Companies
                .GetSingleAsync(x => x.CompanyId == companyId);

            if (companies == null)
            {
                throw new AppNotFoundException("No se encontró la empresa solicitada");
            }

            _unitOfWork.Companies.Update(companies, edited);
            _unitOfWork.SaveChanges();
        }
    }
}
