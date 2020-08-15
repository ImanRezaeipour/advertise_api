using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Companies
{
    public interface ICompanyConversationService
    {
        Task<int> CountByRequestAsync(CompanyConversationSearchModel model);
        Task CreateByViewModelAsync(CompanyConversationCreateModel model);
        Task DeleteByIdAsync(Guid conversationId);
        Task EditByViewModelAsync(CompanyConversationEditModel model);
        Task<CompanyConversation> FindByIdAsync(Guid conversationId);
        Task<IList<CompanyConversation>> GetByRequestAsync(CompanyConversationSearchModel model);
        Task<List<CompanyConversationModel>> GetListByUserIdAsync(Guid userId);
        Task<List<SelectList>> GetUsersAsSelectListAsync();
        IQueryable<CompanyConversation> QueryByRequest(CompanyConversationSearchModel model);
        Task SeedAsync();
    }
}