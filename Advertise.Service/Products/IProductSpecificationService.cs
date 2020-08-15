using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Products
{
    public interface IProductSpecificationService
    {
        Task<int> CountByRequestAsync(ProductSpecificationSearchModel model);
        Task CreateByViewModelAsync(ProductSpecificationCreateModel model);
        Task DeleteByIdAsync(Guid productSpecId);
        Task EditByViewModelAsync(ProductSpecificationEditModel model);
        Task<ProductSpecification> FindByIdAsync(Guid productSpecificationId);
        Task<IList<ProductSpecification>> GetByProductIdAsync(Guid productId);
        Task<IList<ProductSpecification>> GetByRequestAsync(ProductSpecificationSearchModel model);
        IQueryable<ProductSpecification> QueryByRequest(ProductSpecificationSearchModel model);
        Task RemoveRangeAsync(IList<ProductSpecification> productSpecifications);
        Task SeedAsync();
    }
}