using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Permissions;
using Advertise.Core.Model.Permissions;
using Advertise.Core.Objects;

namespace Advertise.Service.Permissions
{
    public interface IPermissionService
    {
        Task<IList<PermissionModel>> GetAllPermissionsAsync();
        Task<IList<Permission>> GetPermissionsByRoleIdAsync(Guid roleId);
        IQueryable<Permission> QueryByRequest(PermissionSearchModel model);
        Task<int> CountByRequestAsync(PermissionSearchModel model);
        Task<IList<Permission>> GetByRequestAsync(PermissionSearchModel model);
        Task<Permission> FindByIdAsync(Guid permissionId);
        Task DeleteByIdAsync(Guid permissionId);
        Task EditByViewModelAsync(PermissionEditModel model);
        Task CreateByViewModelAsync(PermissionCreateModel model);
        Task<object> GetAllTreeAsync();
        Task<IList<Guid>> GetIdsByNamesAsync(IList<string> names);
        Task<IList<JsTreeObject>> GetAllTreeByRoleIdAsync(Guid roleId);
    }
}