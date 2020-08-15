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
    public interface ICompanyImageService
    {
        Task EditApproveByViewModelAsync(CompanyImageEditModel model);
        Task<int> CountAllByCompanyIdAsync(Guid companyId);
        Task<int> CountByRequestAsync(CompanyImageSearchModel model);
        Task CreateByViewModelAsync(CompanyImageCreateModel model);
        Task DeleteByIdAsync(Guid companyImageId, bool isCurrentUser = false);
        void EditAsync(CompanyImageEditModel model, CompanyImage companyImage);
        Task EditByViewModelAsync(CompanyImageEditModel model, bool isCurrentUser = false);
        Task<CompanyImage> FindByIdAsync(Guid companyImageId, Guid? userId = null);
        Task<IList<CompanyImage>> GetApprovedsByCompanyIdAsync(Guid companyId);
        CompanyImage GetById(Guid companyImageId);
        Task<IList<CompanyImage>> GetByRequestAsync(CompanyImageSearchModel model);
        Task<IList<FineUploaderObject>> GetFilesAsFineUploaderModelByIdAsync(Guid companyImageId);
        Task<bool> IsMineAsync(Guid companyImageId);
        IQueryable<CompanyImage> QueryByRequest(CompanyImageSearchModel model);
        Task EditRejectByViewModelAsync(CompanyImageEditModel model);
        Task RemoveRangeAsync(IList<CompanyImage> companyImages);
        Task SetStateByIdAsync(Guid id, StateType state);
    }
}