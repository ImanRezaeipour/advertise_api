using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Products
{
    public interface IProductTagService
    {
        Task<int> CountAllByProductIdAsync(Guid productId);
        Task<int> CountByRequestAsync(ProductTagSearchModel model);
        Task CreateByViewModelAsync(ProductTagCreateModel model);
        Task DeleteByIdAsync(Guid productTagId);
        Task EditByViewModelAsync(ProductTagEditModel model);
        Task<ProductTag> FindByIdAsync(Guid productTagId);
        Task<IList<ProductTag>> GetByProductIdAsync(Guid productId);
        Task<IList<ProductTag>> GetByRequestAsync(ProductTagSearchModel model);
        Task<ProductTagListModel> GetTagsByProductIdAsync(Guid productId);
        Task<ProductTagListModel> ListByRequestAsync(ProductTagSearchModel model);
        IQueryable<ProductTag> QueryByRequest(ProductTagSearchModel model);
        Task RemoveRangeAsync(IList<ProductTag> productTags);
        Task SeedAsync();
    }
}