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
    public interface ICompanyAttachmentService
    {
        Task EditApproveByViewModelAsync(CompanyAttachmentEditModel model);
        Task<int> CountByRequestAsync(CompanyAttachmentSearchModel model);
        Task CreateByViewModelAsync(CompanyAttachmentCreateModel model);
        Task DeleteByIdAsync(Guid companyAttachmentId, bool isCurrentUser = false);
        Task EditAsync(CompanyAttachmentEditModel model, CompanyAttachment companyAttachment);
        Task EditByViewModelAsync(CompanyAttachmentEditModel model, bool isCurrentUser = false);
        Task<CompanyAttachment> FindByIdAsync(Guid companyAttachmentId, Guid? userId = null);
        Task<IList<CompanyAttachment>> GetApprovedByCompanyIdAsync(Guid companyId);
        CompanyAttachment GetById(Guid companyAttachmentId);
        Task<IList<CompanyAttachment>> GetByRequestAsync(CompanyAttachmentSearchModel model);
        Task<IList<FineUploaderObject>> GetFilesAsFineUploaderModelByIdAsync(Guid companyAttachmentId);
        Task<bool> IsMineAsync(Guid companyAttachmentId);
        IQueryable<CompanyAttachment> QueryByRequest(CompanyAttachmentSearchModel model);
        Task EditRejectByViewModelAsync(CompanyAttachmentEditModel model);
        Task RemoveRangeAsync(IList<CompanyAttachment> companyAttachments);
        Task SetStateByIdAsync(Guid id, StateType state);
    }
}