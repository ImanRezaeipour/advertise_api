using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Factory.Products
{
    public interface IProductSpecificationFactory
    {
        Task<IList<ProductSpecificationCreateModel>> PrepareCreateModelAsync(Guid categoryId);
        Task<ProductSpecificationDetailModel> PrepareDetailModelAsync(Guid productSpecificationId);
        Task<IList<ProductSpecificationEditModel>> PrepareEditModelAsync(Guid productId);
        Task<ProductSpecificationListModel> PrepareListModelAsync(ProductSpecificationSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}