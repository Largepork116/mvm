using System.IO;
using System.Threading.Tasks;

namespace DavidMorales.Domain.Interfaces.Services
{
    public interface IFileStoreService
    {
        Task<string> AddAsync(string uplodedFileName, Stream file);
        Task DeleteAsync(string fileName);
        Task<string> UpdateAsync(string uplodedFileName, string existingFileName, Stream file);
        Task<string> GetFullPath(string fileName);
    }
}
