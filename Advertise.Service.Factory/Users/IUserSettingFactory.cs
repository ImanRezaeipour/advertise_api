using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Users;

namespace Advertise.Service.Factory.Users
{
    public interface IUserSettingFactory
    {
        Task<UserSettingEditModel> PrepareEditModelAsync(Guid? id = null, bool isCurrentUser = false, UserSettingEditModel modelPrepare = null);
        Task<UserSettingListModel> PrepareListModelAsync(UserSettingSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}