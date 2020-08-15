using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Objects;

namespace Advertise.Service.Companies
{
    public interface ICompanyOfficialService
    {
        Task ApproveByViewModelAsync(CompanyOfficialEditModel model);
        Task<int> CountByRequestAsync(CompanyOfficialSearchModel model);
        Task CreateByViewModelAsync(CompanyOfficialCreateModel model);
        Task<IList<FineUploaderObject>> GetFileBusinessLicenseAsFineUploaderModelByIdAsync(Guid companyOfficialId);
        Task<IList<FineUploaderObject>> GetFileNationalCardAsFineUploaderModelByIdAsync(Guid companyOfficialId);
        Task<IList<FineUploaderObject>> GetFileOfficialNewspaperAddressAsFineUploaderModelByIdAsync(Guid companyOfficialId);
        Task EditByViewModelAsync(CompanyOfficialEditModel model);
        Task<CompanyOfficial> FindByIdAsync(Guid companyOfficialId);
        Task<IList<CompanyOfficial>> GetByRequestAsync(CompanyOfficialSearchModel model);
        IQueryable<CompanyOfficial> QueryByRequestAsync(CompanyOfficialSearchModel model);
        Task RejectByViewModelAsync(CompanyOfficialEditModel model);
    }
}