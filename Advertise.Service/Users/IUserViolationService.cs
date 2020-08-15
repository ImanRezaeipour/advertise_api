using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Users;
using Advertise.Core.Model.Users;

namespace Advertise.Service.Users
{
    public interface IUserViolationService
    {
        Task<int> CountByRequestAsync(UserViolationSearchModel model);
        Task CreateByViewModelAsync(UserViolationCreateModel model);
        Task DeleteByIdAsync(Guid userViolationId);
        Task EditByViewModelAsync(UserViolationEditModel model);
        Task<UserViolation> FindByIdAsync(Guid userViolationId);
        Task<IList<UserViolation>> GetByRequestAsync(UserViolationSearchModel model);
        IQueryable<UserViolation> QueryByRequest(UserViolationSearchModel model);
    }
}