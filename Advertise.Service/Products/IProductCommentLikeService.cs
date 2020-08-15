using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Products;
using Advertise.Core.Domain.Users;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Products
{
    public interface IProductCommentLikeService
    {
        Task<int> CountByRequestAsync(ProductCommentLikeSearchModel model);
        Task<ProductCommentLike> FindByIdAsync(Guid productCommentId, Guid userId);
        Task<IList<ProductCommentLike>> GetByRequestAsync(ProductCommentLikeSearchModel model);
        Task<IList<User>> GetUsersByCompanyIdAsync(Guid questionId);
        Task<bool> IsLikeCurrentUserByIdAsync(Guid productCommentId);
        IQueryable<ProductCommentLike> QueryByRequest(ProductCommentLikeSearchModel model);
        Task SeedAsync();
        Task ToggleCurrentUserByIdAsync(Guid productCommentId);
    }
}