using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Factory.Products
{
    public interface IProductFeatureFactory
    {
        Task<ProductFeatureDetailModel> PrepareDetailModelAsync(Guid productFeatureId);
        Task<ProductFeatureEditModel> PrepareEditModelAsync(Guid productFeatureId);
        Task<ProductFeatureListModel> PrepareListModelAsync(ProductFeatureSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}