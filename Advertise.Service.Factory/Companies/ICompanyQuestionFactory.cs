using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Factory.Companies
{
    public interface ICompanyQuestionFactory
    {
        Task<CompanyQuestionDetailModel> PrepareDetailModelAsync(Guid companyQuestionId);
        Task<CompanyQuestionEditModel> PrepareEditViewModelAsync(Guid companyQuestionId);
        Task<CompanyQuestionListModel> PrepareListModelAsync(CompanyQuestionSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}