using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Users;
using Advertise.Core.Model.Users;

namespace Advertise.Service.Users
{
    public interface IUserBudgetService
    {
        Task<int> CountByRequestAsync(UserBudgetSearchModel model);
        Task CreateByViewModelAsync(UserBudgetCreateModel model);
        Task DeleteByIdAsync(Guid userBudgetId);
        Task EditByViewModelAsync(UserBudgetEditModel model);
        Task<UserBudget> FindAsync(Guid userBudgetId);
        IQueryable<UserBudget> QueryByRequest(UserBudgetSearchModel model);
        Task<IList<UserBudget>> GetByRequestAsync(UserBudgetSearchModel model);
        Task SeedAsync();
    }
}