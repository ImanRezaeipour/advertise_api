using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Products;
using Advertise.Core.Managers.File;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Products
{
    public interface IProductImageService
    {
        Task<int> CountAllByProductIdAsync(Guid productId);
        Task<int> CountByRequestAsync(ProductImageSearchModel model);
        Task CreateByViewModelAsync(ProductImageCreateModel model);
        Task DeleteByIdAsync(Guid productImageId);
        Task EditByViewModelAsync(ProductImageEditModel model);
        Task<ProductImage> FindByIdAsync(Guid productImageId);
        IQueryable<ProductImage> QueryByRequest(ProductImageSearchModel model);
        Task<List<FileModel>> GetByProductIdAsFileModelAsync(Guid productId);
        Task<IList<ProductImage>> GetByProductIdAsync(Guid productId);
        Task<IList<ProductImage>> GetByRequestAsync(ProductImageSearchModel model);
        Task<ProductImageListModel> ListByRequestAsync(ProductImageSearchModel model);
        Task RemoveRangeAsync(IList<ProductImage> productImages);
    }
}