using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Factory.Companies
{
    public interface ICompanyReviewFactory
    {
        Task<CompanyReviewDetailModel> PrepareDetailModelAsync(Guid companyReviewId);
        Task<CompanyReviewEditModel> PrepareEditViewModelAsync(Guid companyReviewId);
        Task<CompanyReviewListModel> PrepareListModelAsync(CompanyReviewSearchModel model, bool isCurrentUser = false, Guid? userId = null);
        Task<CompanyReviewCreateModel> PrepareCreateModelAsync(CompanyReviewCreateModel modelPrepare = null);
    }
}