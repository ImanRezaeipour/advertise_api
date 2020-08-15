using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Advertise.Core.Extensions
{
    public static class PagingExtension
    {
        #region Public Methods

        public static async Task<List<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex = 0, int pageSize = 10)
        {
            if (pageIndex == 0)
                pageIndex = 1;
            if (pageSize == 0)
                pageSize = 10;
            var skip = (pageIndex - 1) * pageSize;
            var take = pageSize;

            source = source.Skip(skip).Take(take);
            return await source.ToListAsync();
        }
        
        //public static async Task<List<T>> ToPagedListAsync<T>(this IQueryable<T> source, BaseSearchRequest request)
        //{
        //    if (request.PageIndex == 0)
        //        request.PageIndex = 1;
        //    if (request.PageSize == 0)
        //        request.PageSize = PageSize.Count10;
        //    return await source.ToPagedListAsync(request.PageIndex, request.PageSize);
        //}

        #endregion Public Methods
    }
}