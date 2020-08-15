using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Factory.Products
{
    public interface IProductCommentLikeFactory
    {
        Task<ProductCommentLikeListModel> PrepareListViewModelAsync(ProductCommentLikeSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}