using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Factory.Companies
{
    public interface ICompanyBalanceFactory
    {
        Task<CompanyBalanceCreateModel> PrepareCreateModelAsync(CompanyBalanceCreateModel viewModelPrepare = null);
        Task<CompanyBalanceEditModel> PrepareEditModelAsync(Guid companyBalanceId, CompanyBalanceEditModel modelPrepare= null);
        Task<CompanyBalanceListModel> PrepareListModelAsync(CompanyBalanceSearchModel model, bool isCurrentUser = false, Guid? userId = null);
        Task<CompanyBalanceViewModel> PrepareModelAsync();
    }
}