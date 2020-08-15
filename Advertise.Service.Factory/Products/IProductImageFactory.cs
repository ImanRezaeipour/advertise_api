using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Factory.Products
{
    public interface IProductImageFactory
    {
        Task<ProductImageDetailModel> PrepareDetailModelAsync(Guid productImageId);
        Task<ProductImageEditModel> PrepareEditModelAsync(Guid productImageId);
        Task<ProductImageListModel> PrepareListModelAsync(ProductImageSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}