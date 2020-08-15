using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Users;

namespace Advertise.Service.Factory.Users
{
    public interface IUserNotificationFactory
    {
        Task<UserNotificationListModel> PrepareListModelAsync(UserNotificationSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}