using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Products
{
    public interface IProductReviewService
    {
        Task<int> CountByRequestAsync(ProductReviewSearchModel model);
        Task CreateByViewModelAsync(ProductReviewCreateModel model);
        Task DeleteByIdAsync(Guid productReviewId);
        Task EditApproveByViewModelAsync(ProductReviewEditModel model);
        Task EditByViewModelAsync(ProductReviewEditModel model);
        Task EditRejectByViewModelAsync(ProductReviewEditModel model);
        Task<ProductReview> FindByIdAsync(Guid productReviewId);
        Task<IList<ProductReview>> GetByProductIdAsync(Guid productId);
        Task<IList<ProductReview>> GetByRequestAsync(ProductReviewSearchModel model);
        Task<IList<SelectList>> GetProductIdAsync();
        IQueryable<ProductReview> QueryByRequest(ProductReviewSearchModel model);
        Task RemoveRangeAsync(IList<ProductReview> productReviews);
        Task SeedAsync();
    }
}