using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Users;
using Advertise.Core.Model.Users;

namespace Advertise.Service.Users
{
    public interface IUserOperationServive
    {
        Task<int> CountByRequest(UserOperatorSearchModel model);
        Task CreateByModelAsync(UserOperator userOperator);
        Task<UserOperator> FindAsync(Guid userOperatorId);
        Task<UserOperator> FindByUserIdAsync(Guid userId);
        Task<IList<UserOperator>> GetByRequestAsync(UserOperatorSearchModel model);
        IQueryable<UserOperator> QueryByrequest(UserOperatorSearchModel model);
    }
}