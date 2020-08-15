using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Factory.Companies
{
    public interface ICompanyVideoFactory
    {
        Task<CompanyVideoEditModel> PrepareEditModelAsync(Guid companyVideoId, bool applyCurrentUser = false);
        Task<CompanyVideoListModel> PrepareListModelAsync(CompanyVideoSearchModel model, bool isCurrentUser = false, Guid? userId = null);
        Task<CompanyVideoDetailModel> PrepareDetailModelAsync(Guid companyVideoId);
    }
}