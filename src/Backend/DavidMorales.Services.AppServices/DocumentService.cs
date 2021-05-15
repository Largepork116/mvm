using DavidMorales.Domain.Entities;
using DavidMorales.Domain.Exceptions;
using DavidMorales.Domain.Interfaces;
using DavidMorales.Domain.Interfaces.Services;

using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DavidMorales.Services.AppServices
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DocumentService> _logger;

        public DocumentService(
            IUnitOfWork unitOfWork,
            ILogger<DocumentService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task<IEnumerable<Document>> GetAsync()
        {
            var documents = await _unitOfWork.Documents.GetAsync(
                null, 
                null, 
                i=>i.ExternalFile,
                i=>i.InternalFile,
                i=>i.Sender.Company,
                i=>i.Addressee.Company);
            return documents;
        }

        public async Task<IEnumerable<Document>> GetByAddresseeAsync(int userId)
        {
            var documents = await _unitOfWork.Documents.GetAsync(
                x => x.Addressee.User.Id == userId,
                null,
                i => i.ExternalFile,
                i => i.InternalFile,
                i => i.Sender.Company,
                i => i.Addressee.Company);
            return documents;
        }

        public async Task CreateAsync(Document document)
        {
            await _unitOfWork.Documents.AddAsync(document);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
