using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Factory.Companies
{
    public interface ICompanySlideFactory
    {
        Task<CompanySlideCreateModel> PrepareCreateModelAsync();
        Task<CompanySlideEditModel> PrepareEditModelAsync(Guid companySlideId);
        Task<CompanySlideBulkEditModel> PrepareBulkEditModelAsync();
        Task<CompanySlideListModel> PrepareListModelAsync(CompanySlideSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}