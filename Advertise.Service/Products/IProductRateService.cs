using System;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Products
{
    public interface IProductRateService
    {
        Task<int> CountByRequestAsync(ProductRateSearchModel model);
        Task CreateByViewModelAsync(ProductRateCreateModel model);
        Task<decimal> GetRateByProductIdAsync(Guid productId);
        Task<int> GetUserCountByProductIdAsync(Guid productId);
        Task<int> GetRateByCurrentUserAsync(Guid productId);
        Task<bool> IsRatedCurrentUserByProductAsync(Guid productId);
        IQueryable<ProductRate> QueryByRequest(ProductRateSearchModel model);
        Task<decimal> RateByRequestAsync(ProductRateSearchModel model);
    }
}