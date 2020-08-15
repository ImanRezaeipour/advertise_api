using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Users;

namespace Advertise.Service.Factory.Users
{
    public interface IUserBudgetFactory
    {
        Task<UserBudgetListModel> PrepareListModelAsync(bool isCurrentUser = false, Guid? userId = null);
    }
}