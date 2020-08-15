using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Factory.Companies
{
    public interface ICompanyAttachmentFactory
    {
        Task<CompanyAttachmentEditModel> PrepareEditModelAsync(Guid companyAttachmentId, bool applyCurrentUser = false);
        Task<CompanyAttachmentListModel> PrepareListModelAsync(CompanyAttachmentSearchModel model, bool isCurrentUser = false, Guid? userId = null);
        Task<CompanyAttachmentDetailModel> PrepareDetailModelAsync(Guid companyAttachmentId);
    }
}