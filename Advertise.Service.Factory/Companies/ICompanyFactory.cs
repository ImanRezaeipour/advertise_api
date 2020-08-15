using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Companies;
using Advertise.Core.Model.General;
using Advertise.Core.Model.Products;

namespace Advertise.Service.Factory.Companies
{
    public interface ICompanyFactory
    {
        Task<CompanyDetailInfoModel> PrepareDetailInfoModelAsync(string companyAlias);
        Task<CompanyDetailModel> PrepareDetailModelAsync(string companyAlias, string slug);
        Task<CompanyEditModel> PrepareEditModelAsync(string companyAlias = null, bool applyCurrentUser = false, CompanyEditModel modelApply = null);
        Task<CompanyImageListModel> PrepareImageListViewModelAsync(Guid companyId);
        Task<CompanyListModel> PrepareListModelAsync(CompanySearchModel model, bool isCurrentUser = false, Guid? userId = null);
        Task<ProductListModel> PrepareProductListModelAsync(Guid companyId);
        Task<ProfileMenuModel> PrepareProfileMenuModelAsync();
        Task<CompanyReviewListModel> PrepareReviewListModelAsync(Guid companyId);
    }
}