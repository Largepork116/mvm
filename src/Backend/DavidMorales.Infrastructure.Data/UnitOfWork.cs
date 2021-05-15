using DavidMorales.Domain.Interfaces;
using DavidMorales.Domain.Interfaces.Repositories;
using DavidMorales.Infrastructure.Context;
using DavidMorales.Infrastructure.Data.Repositories;

using System.Threading.Tasks;

namespace DavidMorales.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppContext _context;

        public IUserRepository Users { get; }
        public ICompanyRepository Companies { get;  }
        public IPersonRepository People { get;  }
        public IDocumentRepository Documents { get;  }
        public ILogDataChangeRepository LogsDataChanges { get; }

        public UnitOfWork(AppContext context)
        {
            _context = context;

            Users = new UserRepository(_context);
            Companies = new CompanyRepository(_context);
            People = new PersonRepository(_context);
            Documents = new DocumentRepository(_context);
            LogsDataChanges = new LogDataChangeRepository(_context);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
