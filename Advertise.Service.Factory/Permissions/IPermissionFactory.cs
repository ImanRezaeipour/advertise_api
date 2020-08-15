using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Permissions;

namespace Advertise.Service.Factory.Permissions
{
    public interface IPermissionFactory
    {
        Task<PermissionEditModel> PrepareEditModelAsync(Guid permissionId);
        Task<PermissionListModel> PrepareListModel(PermissionSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}