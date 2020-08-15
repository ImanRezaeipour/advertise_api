using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Factory.Companies
{
    public interface ICompanyImageFactory
    {
        Task<CompanyImageEditModel> PrepareEditModelAsync(Guid companyImageId, bool applyCurrentUser = false, CompanyImageEditModel  modelPrepare = null);
        Task<CompanyImageListModel> PrepareListModelAsync(CompanyImageSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}