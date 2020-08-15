using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Products
{
    public interface IProductNotifyService
    {
        Task<int> CountByRequestAsync(ProductNotifySearchModel model);
        Task CreateByViewModelAsync(ProductNotifyModel model);
        Task DeleteByProductIdAsync(Guid productId, bool? applyCurrentUser = false);
        Task<ProductNotify> FindByProductIdAync(Guid productId, bool? applyCurrentUser = false);
        Task<IList<Guid?>> GetUsersByProductIdAsync(Guid productId);
        Task<bool> IsExistAsync(Guid productId, Guid userId);
        Task<bool> IsExistByProductIdAsync(Guid productId, bool? applyCurrentUser);
        IQueryable<ProductNotify> QueryByRequest(ProductNotifySearchModel model);
        Task ToggleByProductIdAsync(Guid productId);
    }
}