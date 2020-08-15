using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Companies
{
    public interface ICompanyAttachmentFilService
    {
        Task<int> CountByRequestAsync(CompanyAttachmentFileSearchModel model);
        Task<CompanyAttachmentFile> FindByIdAsync(Guid companyAttachmentFileId);
        Task<IList<CompanyAttachmentFile>> GetByRequestAsync(CompanyAttachmentFileSearchModel model);
        IQueryable<CompanyAttachmentFile> QueryByRequest(CompanyAttachmentFileSearchModel model);
    }
}