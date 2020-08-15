using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Products;
using Advertise.Core.Domain.Users;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Products
{
    public interface IProductLikeService
    {
        Task<int> CountAllLikedByProductIdAsync(Guid productId);
        Task<int> CountByProductIdAsync(Guid productId);
        Task<List<ProductLikeModel>> GetByProductsAsync(List<Guid> productsId);
        Task<int> CountByRequestAsync(ProductLikeSearchModel model);
        Task  CreateByViewModelAsync(ProductLikeCreateModel model);
        Task RemoveRangeByproductLikesAsync(IList<ProductLike> productLikes);
        Task  DeleteByIdAsync(Guid productLikeId);
        Task  EditByViewModelAsync(ProductLikeEditModel model);
        Task<ProductLike> FindByIdAsync(Guid productLikeId);
        Task<ProductLike> FindByProductIdAsync( Guid productId, Guid? userId =null);
        Task<IList<ProductLike>> GetByRequestAsync(ProductLikeSearchModel model);
        Task<List<Guid>> GetMostLikedProductIdAsync();
        Task<IList<User>> GetUsersByProductAsync(Guid productId);
        Task<bool> IsLikeCurrentUserByProductIdAsync(Guid productId);
        Task<bool> IsLikeByProductIdAsync(Guid productId, Guid userId);
        IQueryable<ProductLike> QueryByRequest(ProductLikeSearchModel model);
        Task  RemoveRangeAsync(IList<ProductLike> productLikes);
        Task  SeedAsync();
        Task ToggleCurrentUserByProductIdAsync(Guid productId);
    }
}