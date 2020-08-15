using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Factory.Products
{
    public interface IProductLikeFactory
    {
        Task<ProductLikeListModel> PrepareListModelAsync(ProductLikeSearchModel model, bool isCurrentUser = false, Guid? userId = null);
        Task<ProductLikeListModel> PrepareLikerListModelAsync(ProductLikeSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}