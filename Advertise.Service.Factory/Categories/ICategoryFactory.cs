using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Categories;
using Advertise.Core.Model.Companies;
using Advertise.Core.Model.ExportImport;
using Advertise.Core.Model.General;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Factory.Categories
{
    public interface ICategoryFactory
    {
        Task<CategoryBreadCrumbModel> PrepareBreadCrumbModelAsync(Guid categoryId, string currentTitle, bool? isAllSearch);
        Task<CompanyListModel> PrepareCompanyListModelAsync(Guid categoryId);
        Task<CategoryCreateModel> PrepareCreateModelAsync();
        Task<CategoryDetailModel> PrepareDetailModelAsync(string categoryAlias, string slug);
        Task<CategoryEditModel> PrepareEditModelAsync(string categoryAlias);
        Task<CategoryListModel> PrepareListModelAsync(CategorySearchModel request, bool isCurrentUser = false, Guid? userId = null);
        Task<MainMenuModel> PrepareMainMenuModelAsync();
        Task<ProductListModel> PrepareProductListModelAsync(Guid categoryId);
        Task<ExportIndexModel> PrepareExportIndexModelAsync();
        Task<ImportIndexModel> PrepareImportIndexModelAsync();
    }
}