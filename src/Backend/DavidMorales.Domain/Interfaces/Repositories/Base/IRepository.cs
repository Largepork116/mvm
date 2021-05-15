using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DavidMorales.Domain.Interfaces.Repositories.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
                                            Func<IQueryable<TEntity>,
                                            IOrderedQueryable<TEntity>> orderBy = null,
                                            params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> GetIncludingAsync(Expression<Func<TEntity, bool>> filter = null,
                                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                         params string[] includes);

        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter,
                                 params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetSingleIncludingAsync(Expression<Func<TEntity, bool>> filter,
                                params string[] includes);

        Task<TEntity> FindAsync<TId>(TId id);
        Task AddAsync(TEntity entity);
        Task AddAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity, TEntity edited);
        void Remove(TEntity entity);
        Task<long> CountAsync(Expression<Func<TEntity, bool>> filter = null);
    }
}
