using DavidMorales.Domain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DavidMorales.Domain.Interfaces.Services
{
    public interface IDocumentService
    {
        Task<IEnumerable<Document>> GetAsync();
        Task<IEnumerable<Document>> GetByAddresseeAsync(int userId);
        Task CreateAsync(Document document);
    }
}
