using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Factory.Products
{
    public interface IProductTagFactory
    {
        Task<ProductTagListModel> PrepareListModelAsync(ProductTagSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}