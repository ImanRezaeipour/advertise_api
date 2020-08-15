using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Advertise.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeProperties<T>(this IQueryable<T> queryable, params Expression<Func<T, object>>[] includeProperties)
        {
            if (queryable == null)
                throw new ArgumentNullException(nameof(queryable));

            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
                queryable = queryable.Include(includeProperty);

            return queryable;
        }
    }
}