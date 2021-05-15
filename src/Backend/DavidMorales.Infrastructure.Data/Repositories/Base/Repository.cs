using DavidMorales.Domain.Interfaces.Repositories.Base;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DavidMorales.Infrastructure.Data.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        protected string errorMessage = string.Empty;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetIncludingAsync(Expression<Func<TEntity, bool>> filter = null,
                                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                         params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return await query.AsNoTracking().IncludeMultiple(includes).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
                                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                         params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter,
                                 params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(filter).AsNoTracking().FirstOrDefaultAsync();
        }


        public async Task<TEntity> GetSingleIncludingAsync(Expression<Func<TEntity, bool>> filter,
                                 params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            return await query.Where(filter).IncludeMultiple(includes).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<TEntity> FindAsync<TId>(TId id)
        {
            return await _context.Set<TEntity>()
                .FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            await _dbSet.AddAsync(entity);
        }

        public async Task AddAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (entities.Count() == 0)
            {
                throw new ArgumentNullException("entity");
            }

            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(TEntity entity, TEntity edited)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                _dbSet.Attach(entity);
            }
            catch (Exception ex)
            {
                _context.Entry(entity).State = EntityState.Detached;
                _dbSet.Attach(entity);
            }

            _context.Entry(entity).CurrentValues.SetValues(edited);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            return await query.CountAsync();
        }
    }
}
