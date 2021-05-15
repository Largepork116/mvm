using DavidMorales.Domain.Entities;
using DavidMorales.Domain.Interfaces.Repositories;
using DavidMorales.Infrastructure.Data.Repositories.Base;

using Microsoft.EntityFrameworkCore;

namespace DavidMorales.Infrastructure.Data.Repositories
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(DbContext context) : base(context)
        {

        }
    }
}
