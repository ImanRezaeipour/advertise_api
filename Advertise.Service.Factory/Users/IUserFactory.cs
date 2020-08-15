using System;
using System.Threading.Tasks;
using Advertise.Core.Model.General;
using Advertise.Core.Model.Users;

namespace Advertise.Service.Factory.Users
{
    public interface IUserFactory
    {
        Task<DashboardHeaderModel> PrepareDashboardHeaderModelAsync();
        Task<UserDetailModel> PrepareDetailModelAsync(string userName);
        Task<UserEditModel> PrepareEditModelAsync(string userName = null, bool isCurrentUser = false, UserEditModel modelPrepare = null);
        Task<UserHeaderModel> PrepareHeaderModelAsync();
        Task<UserListModel> PrepareListModelAsync(UserSearchModel model, bool isCurrentUser = false, Guid? userId = null);
        Task<ProfileHeaderModel> PrepareProfileHeaderModelAsync();
    }
}