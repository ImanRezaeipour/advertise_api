using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Factory.Products
{
    public interface IProductCommentFactory
    {
        Task<ProductCommentDetailModel> PrepareDetailModelAsync(Guid productCommentId);
        Task<ProductCommentEditModel> PrepareEditModelAsync(Guid productCommentId, bool applyCurrentUser = false);
        Task<ProductCommentListModel> PrepareListModelAsync(ProductCommentSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}