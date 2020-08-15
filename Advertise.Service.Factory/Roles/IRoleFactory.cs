using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Roles;

namespace Advertise.Service.Factory.Roles
{
    public interface IRoleFactory
    {
        Task<RoleEditModel> PrepareEditModelAsync(Guid roleId);
        Task<RoleListModel> PrepareListModelAsync(RoleSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}