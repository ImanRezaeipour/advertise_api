using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Companies;
using Advertise.Core.Model.General;

namespace Advertise.Service.Factory.Common
{
    public interface ICommonFactory
    {
        Task<PanelBoardModel> PrepareDashBoardModelAsync();
        Task<PanelBoardModel> PrepareDashBoardModelAsync(Guid userId);
        Task<LandingPageModel> PrepareLandingPageModelAsync();
        Task PrepareCompanyModelAsync(IList<CompanyModel> listCompany);
        Task<ProfileModel> PrepareProfileViewModelAsync();
    }
}