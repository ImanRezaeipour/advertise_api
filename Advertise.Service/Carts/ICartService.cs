using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Carts;
using Advertise.Core.Model.Carts;

namespace Advertise.Service.Carts
{
    public interface ICartService
    {
        Task<int> CountByCurrentUserAsync();
        Task<int> CountByRequestAsync(CartSearchModel request);
        Task CreateByIdAsync(Guid productId);
        Task DeleteByCurrentUserAsync();
        Task DeleteByIdAsync(Guid bagId);
        Task DeleteByProductIdAsync(Guid productId);
        Task<Cart> FindByIdAsync(Guid bagId);
        Task<Cart> FindByProductIdAsync(Guid productId, Guid userId);
        Task<Cart> FindByUserIdAsync(Guid userId);
        Task<IList<Cart>> GetByRequestAsync(CartSearchModel request);
        Task<IList<Cart>> GetByUserIdAsync(Guid userId);
        Task<bool> IsExistByProductIdAsync(Guid productId, Guid? userId = null);
        IQueryable<Cart> QueryByRequest(CartSearchModel request);
        Task RemoveRangeAsync(IList<Cart> bags);
        Task SetProductCountByIdAsync(Guid productId, int productCount);
        Task ToggleByCurrentUserAsync(Guid productId);
    }
}