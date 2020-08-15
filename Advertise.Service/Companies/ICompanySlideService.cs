using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Objects;

namespace Advertise.Service.Companies
{
    public interface ICompanySlideService
    {
        Task<int> CountByRequestAsync(CompanySlideSearchModel model);
        Task CreateByViewModelAsync(CompanySlideCreateModel model);
        Task DeleteByIdAsync(Guid companySlideId);
        Task EditByViewModelAsync(CompanySlideEditModel model);
        Task<CompanySlide> FindByIdAsync(Guid companySlideId);
        Task<IList<CompanySlide>> GetByRequestAsync(CompanySlideSearchModel model);
        IQueryable<CompanySlide> QueryByRequest(CompanySlideSearchModel model);
        Task<IList<FineUploaderObject>> GetFileAsFineUploaderModelByIdAsync(Guid companySlideId);
    }
}