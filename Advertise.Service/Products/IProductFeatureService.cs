using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Products
{
    public interface IProductFeatureService
    {
        Task<int> CountByRequestAsync(ProductFeatureSearchModel model);
        Task CreateByViewModelAsync(ProductFeatureCreateModel model);
        Task DeleteByIdAsync(Guid productFeatureId);
        Task EditByViewModelAsync(ProductFeatureEditModel model);
        Task<ProductFeature> FindByIdAsync(Guid productFeatureId);
        Task<IList<ProductFeature>> GetByRequestAsync(ProductFeatureSearchModel model);
        Task<ProductFeatureListModel> ListByRequestAsync(ProductFeatureSearchModel model);
        IQueryable<ProductFeature> QueryByRequest(ProductFeatureSearchModel model);
        Task RemoveRangeAsync(IList<ProductFeature> productFeatures);
        Task SeedAsync();
    }
}