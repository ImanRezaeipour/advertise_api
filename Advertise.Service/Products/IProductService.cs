using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Locations;
using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Products;
using Advertise.Core.Objects;
using Advertise.Core.Types;

namespace Advertise.Service.Products
{
    public interface IProductService
    {
        Task<decimal?> AverageByRequestAsync(ProductSearchModel model, string aggregateMember);
        Task<bool> CompareCodeAndSlugAsync(string code, string slug);
        Task<bool> IsApprovedAsync(string code);
        Task<int> CountAllAsync();
        Task<int> CountByStateAsync(StateType productState);
        Task<int> CountByCategoryIdAsync(Guid categoryId);
        Task<int> CountByCompanyIdAsync(Guid companyId, StateType? state = null);
        Task<int> CountByRequestAsync(ProductSearchModel model, bool isCurrentUser = false, Guid? userId = null);
        Task<int> CountByUserIdAsync(Guid userId, StateType state);
        Task CreateByViewModelAsync(ProductCreateModel model);
        Task CreateBulkByViewModelAsync(ProductBulkCreateModel model);
        Task<bool> AnyByRequestAsync(ProductSearchModel model);
        Task EditBulkByViewModelAsync(ProductBulkEditModel model);
        Task EditCatalogByViewModelAsync(ProductEditCatalogModel model);
        Task DeleteByCodeAsync(string productCode, bool isCurrentUser = false);
        Task<IList<ProductModel>> GetByIdsAsync(IEnumerable<Guid?> listId);
        Task DeleteByUserIdAsync(Guid userId);
        Task EditApproveByViewModelAsync(ProductEditModel model);
        Task EditAsync(ProductEditModel model, Product originalProduct);
        Task EditByViewModelAsync(ProductEditModel model, bool isCurrentUser = false);
        Task EditRejectByViewModelAsync(ProductEditModel model);
        Task<Product> FindByIdAsync(Guid productId);
        Task<Product> FindByCodeAsync(string productCode);
        Task<IList<Product>> GetByCodeWithCurrentUser(string productCode);
        Task<Product> FindByUserIdWithCodeAsync(Guid userId, string code);
        Task<string> GenerateCodeAsync();
        Task<Location> GetAddressByIdAsync(Guid productId);
        Task<IList<SelectList>> GetAllCurrentUserAsSelectListItem();
        Task<IList<ProductModel>> GetApprovedByCompanyIdAsync(Guid companyId);
        Product GetByIdAsync(Guid productId);
        Task<IList<Product>> GetByRequestAsync(ProductSearchModel model);
        Task<IList<FineUploaderObject>> GetFileAsFineUploaderModelByIdAsync(Guid productId);
        Task<IList<SelectList>> GetAsSelectListAsync();
        Task<IList<FineUploaderObject>> GetFilesAsFineUploaderModelByIdAsync(Guid productId);
        Task<ProductLikeListModel> GetMyListProductLikeAsync();
        Task<IList<Product>> GetProductsByUserIdAsync(Guid userId);
        Task<bool> IsMineByCodeAsync(string productCode);
        Task<decimal?> MaxByRequestAsync(ProductSearchModel model, Expression<Func<Product, decimal?>> agg);
        Task<string> MaxCodeByRequestAsync(ProductSearchModel model , Expression<Func<Product, string>> code);
        Task<decimal?> MinByRequestAsync(ProductSearchModel model, Expression<Func<Product, decimal?>> agg);
        IQueryable<Product> QueryByRequest(ProductSearchModel model);
        Task<IList<SelectList>> CastQueryDictionaryToRequestValues(Dictionary<string, List<string>> queryDictionary);
        Task<IList<ProductModel>> GetByCatalogIdAsync(Guid catalogId);
        Task<decimal?> SumByRequestAsync(ProductSearchModel model, string aggregateMember);
        Task SetStateByIdAsync(Guid productId, StateType state);
    }
}