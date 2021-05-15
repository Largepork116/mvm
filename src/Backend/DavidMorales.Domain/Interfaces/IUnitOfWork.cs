using DavidMorales.Domain.Interfaces.Repositories;

using System.Threading.Tasks;

namespace DavidMorales.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        ICompanyRepository Companies { get; }
        IPersonRepository People { get; }
        IDocumentRepository Documents { get; }
        ILogDataChangeRepository LogsDataChanges { get; }

        bool SaveChanges();
        Task<bool> SaveChangesAsync();
    }
}
