using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Factory.Products
{
    public interface IProductFactory
    {
        Task<ProductBulkCreateModel> PrepareBulkCreateModelAsync(ProductBulkCreateModel modelPrepare = null);
        Task<ProductBulkEditModel> PrepareBulkEditModelAsync();
        Task<ProductCreateModel> PrepareCreateModelAsync(ProductCreateModel modelPrepare = null);
        Task<ProductDetailModel> PrepareDetailModelAsync(string productCode);
        Task<ProductEditModel> PrepareEditModelAsync(string productCode, bool isMine = false, ProductEditModel modelPrepare = null);
        Task<ProductListModel> PrepareListModelAsync(ProductSearchModel model, bool isCurrentUser = false, Guid? userId = null);
        Task<ProductReviewListModel> PrepareReviewListModelAsync(Guid productId);
        Task<ProductSearchModel> PrepareSearchModelAsync(ProductSearchModel model);
        Task<ProductBulkEditModel> PrepareEditCatalogModelAsync(string productCode, bool isMine = false, ProductBulkEditModel modelPrepare = null);
    }
}