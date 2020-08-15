using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Factory.Companies
{
    public interface ICompanyHourFactory
    {
        Task<CompanyHourEditModel> PrepareEditModelAsync(Guid? companyHourId = null, bool isCurrentUser = false, CompanyHourEditModel modelPrepare = null);
        Task<CompanyHourListModel> PrepareListModRelAsync(CompanyHourSearchModel model,bool isCurrentUser = false, Guid? userId = null);
    }
}