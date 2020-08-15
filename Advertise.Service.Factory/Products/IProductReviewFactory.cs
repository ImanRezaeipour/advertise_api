using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Factory.Products
{
    public interface IProductReviewFactory
    {
        Task<ProductReviewDetailModel> PrepareDetailModelAsync(Guid productReviewId);
        Task<ProductReviewCreateModel> PrepareCreateModelAsync(ProductReviewCreateModel modelPrepare= null);
        Task<ProductReviewEditModel> PrepareEditModelAsync(Guid productReviewId);
        Task<ProductReviewListModel> PrepareListModelAsync(ProductReviewSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}