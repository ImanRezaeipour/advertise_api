using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Companies
{
    public interface ICompanySocialService
    {
        Task<int> CountByRequestAsync(CompanySocialSearchModel model);
        Task CreateByViewModelAsync(CompanySocialCreateModel model);
        Task DeleteByIdAsync(Guid socialId);
        Task EditByViewModelAsync(CompanySocialEditModel model, bool isCurrentUser = false);
        Task<CompanySocial> FindAsync(Guid companySocialId);
        Task<CompanySocial> FindByUserIdAsync(Guid userId);
        IQueryable<CompanySocial> QueryByRequest(CompanySocialSearchModel request);
        Task<IList<CompanySocial>> GetByRequestAsync(CompanySocialSearchModel model);
        Task RemoveRangeAsync(IList<CompanySocial> companySocials);
        Task SeedAsync();
    }
}