using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Factory.Companies
{
    public interface ICompanyConversationFactory
    {
        Task<CompanyConversationEditModel> PrepareEditModelAsync(Guid conversationId);
        Task<CompanyConversationListModel> PrepareListModelAsync(CompanyConversationSearchModel model,bool isCurrentUser = false, Guid? userId = null);
    }
}