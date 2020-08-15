using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Domain.Users;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Companies
{
    public interface ICompanyQuestionLikeService
    {
        Task<int> CountByRequestAsync(CompanyQuestionLikeSearchModel model);
        Task<CompanyQuestionLike> FindAsync(Guid companyId, Guid userId);
        Task<IList<CompanyQuestionLike>> GetByRequestAsync(CompanyQuestionLikeSearchModel model);
        Task<IList<User>> GetUsersByCompanyIdAsync(Guid questionId);
        Task<bool> IsLikeByCurrentUserAsync(Guid questionId);
        Task<bool> IsLikeByUserIdAsync(Guid questionId, Guid userId);
        Task<CompanyQuestionLikeListModel> ListByRequestAsync(CompanyQuestionLikeSearchModel model);
        IQueryable<CompanyQuestionLike> QueryByRequest(CompanyQuestionLikeSearchModel model);
        Task RemoveRangeAsync(IList<CompanyQuestionLike> companyQuestionLikes);
        Task SeedAsync();
        Task SetIsLikeByCurrentUserAsync(Guid questionId, bool isLike);
        Task ToggleLikeByCurrentUserAsync(Guid questionId);
    }
}