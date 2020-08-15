using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Domain.Users;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Companies
{
    public interface ICompanyFollowService
    {
        Task<int> CountAllFollowByCompanyIdAsync(Guid companyId);
        Task<int> CountAsync(Guid comapnyId);
        Task<int> CountByRequestAsync(CompanyFollowSearchModel model);
        Task<CompanyFollow> FindAsync(Guid companyFollowId);
        Task<CompanyFollow> FindByCompanyIdAsync(Guid userId, Guid companyId);
        Task<CompanyFollow> FindByUserIdAsync(Guid userId);
        Task<IList<CompanyFollow>> GetByRequestAsync(CompanyFollowSearchModel model);
        Task<IList<User>> GetUsersByCompanyIdAsync(Guid companyId);
        Task<bool> IsFollowByCurrentUserAsync(Guid companyId);
        Task<bool> IsFollowByUserIdAsync(Guid companyId, Guid userId);
        Task<CompanyFollowListModel> ListByRequestAsync(CompanyFollowSearchModel model, bool isCurrentUser = false, Guid? userId = null);
        IQueryable<CompanyFollow> QueryByRequest(CompanyFollowSearchModel model);
        Task RemoveRangeAsync(IList<CompanyFollow> companyFollows);
        Task SeedAsync();
        Task ToggleFollowCurrentUserByCompanyIdAsync(Guid companyId);
    }
}