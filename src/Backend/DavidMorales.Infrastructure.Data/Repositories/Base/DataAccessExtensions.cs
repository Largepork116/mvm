using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace DavidMorales.Infrastructure.Data.Repositories.Base
{
    internal static class DataAccessExtensions
    {
        internal static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query,
            params string[] includes) where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }
    }
}
