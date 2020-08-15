using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Factory.Companies
{
    public interface ICompanyOfficialFactory
    {
        Task<CompanyOfficialEditModel> PrepareEditModelAsync(Guid companyOfficialId, bool applyCurrentUser = false, CompanyOfficialEditModel modelApply = null);
        Task<CompanyOfficialListModel> PrepareListModelAsync(CompanyOfficialSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}