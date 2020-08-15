using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Advertise.Service.Products
{
    public interface IProductKeywordService
    {
        Task<List<Guid?>> GetIdsByProductIdAsync(Guid productId);
        Task<List<string>> GetTitlesByProductIdAsync(Guid productId);
    }
}