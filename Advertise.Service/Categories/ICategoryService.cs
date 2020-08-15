using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Categories;
using Advertise.Core.Model.Categories;
using Advertise.Core.Model.Common;
using Advertise.Core.Objects;

namespace Advertise.Service.Categories
{
    public interface ICategoryService
    {
        Task<int> CountAllAsync();
        Task<int> CountByRequestAsync(CategorySearchModel request);
        Task CreateByViewModelAsync(CategoryCreateModel viewModel);
        Task DeleteByAliasAsync(string categoryAlias);
        Task DeleteByIdAsync(Guid categoryId);
        Task EditByViewModelAsync(CategoryEditModel viewModel);
        Task<Category> FindByAliasAsync(string categoryAlias);
        Task<Category> FindByCodeAsync(string categoryCode);
        Task<Category> FindByIdAsync(Guid categoryId);
        Task<Category> FindParentAsync(Guid categoryId);
        Task<List<SelectList>> GetAllAsSelectListAsync();
        Task<object> GetAllAsync();
        Task<IList<CategoryModel>> GetAllSalableTypeAsync();
        Task<IList<Category>> GetByRequestAsync(CategorySearchModel request);
        Task<IList<CategoryModel>> GetAsViewModelByRequestAsync(CategorySearchModel request);
        Task<IList<CategoryModel>> GetCategoriesByParentId(Guid parentId);
        Task<CategoryOption> GetCategoryOptionByIdAsync(Guid id);
        Task<IList<Category>> GetChildsByIdAsync(Guid categoryId);
        Task<IList<FineUploaderObject>> GetFileAsFineUploaderModelByIdAsync(Guid categoryId);
        Task<Guid> GetIdByAliasAsync(string alias);
        Task<IList<SelectList>> GetMainCategoriesAsSelectListItemAsync();
        Task<IList<CategoryModel>> GetMainCategoriesAsync();
        Task<IList<Category>> GetParentsByIdAsync(Guid categoryId, bool? withRoot = false);
        Task<IList<CategoryModel>> GetRaletedCategoriesByAliasAsync(string categoryAlias);
        Task<Category> GetRootAsync();
        Task<IList<SelectList>> GetAllowedAsSelectListAsync();
        Task<IList<Select2Object>> GetAllowedAsSelect2ObjectAsync();
        Task<IList<Guid>> GetAllChildsByIdAsync(Guid categoryId);
        Task<IEnumerable<Category>> GetAllChildsByIdAsync(List<Category> categoryList, Category category);
        Task<object> GetSubCategoriesByParentIdAsync(Guid parentId);
        Task<object> GetSubCatergoriesWithRootByIdAsync(Guid categoryId);
        Task<bool> IsCompareNameAndSlugAsync(string alias, string slug);
        Task<bool> IsRootAsync(Guid categoryId);
        IQueryable<Category> QueryByRequest(CategorySearchModel request);
        Task SeedAsync();
        Task FixAllNodesLevel();
        Guid CurrentCategoryId { get; }
        Task<byte[]> GetCategoryAsExcelAsync();
        Task ImportCategoriesFromXlsxAsync(Stream stream);
        Task<byte[]> ExportCategoriesToXlsxAsync(IEnumerable<CategoryModel> categories);
    }
}