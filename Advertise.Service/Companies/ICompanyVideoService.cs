using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Objects;
using Advertise.Core.Types;

namespace Advertise.Service.Companies
{
    public interface ICompanyVideoService
    {
        Task<int> CountAllVideoByCompanyIdAsync(Guid companyId);
        Task<int> CountByRequestAsync(CompanyVideoSearchModel model);
        Task CreateByViewModelAsync(CompanyVideoCreateModel model);
        Task DeleteByIdAsync(Guid companyVideoId, bool isCurrentUser = false);
        Task EditApproveByViewModelAsync(CompanyVideoEditModel model);
        Task EditAsync(CompanyVideoEditModel model, CompanyVideo companyVideo);
        Task EditByViewModelAsync(CompanyVideoEditModel model, bool isCurrentUser = false);
        Task EditRejectByViewModelAsync(CompanyVideoEditModel model);
        Task<CompanyVideo> FindByIdAsync(Guid? companyVideoId = null ,Guid? userId= null);
        Task<IList<CompanyVideo>> GetApprovedByCompanyIdAsync(Guid companyId);
        Task<IList<CompanyVideo>> GetByRequestAsync(CompanyVideoSearchModel model);
        Task<IList<FineUploaderObject>> GetFilesAsFineUploaderModelByIdAsync(Guid companyVideoId);
        IQueryable<CompanyVideo> QueryByRequest(CompanyVideoSearchModel model);
        Task RemoveRangeAsync(IList<CompanyVideo> companyVideos);
        Task SetStateByIdAsync(Guid companyVideoId, StateType state);
    }
}