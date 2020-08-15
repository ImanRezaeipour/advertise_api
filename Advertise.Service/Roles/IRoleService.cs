using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Roles;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Roles;

namespace Advertise.Service.Roles
{
    public interface IRoleService
    {
        bool AutoCommitEnabled { get; set; }
        Task<bool> IsSystemRoleAsync(Guid id);
        Task<int> CountByRequestAsync(RoleSearchModel model);
        Task CreateByViewModelAsync(RoleCreateModel model);
        Task DeleteByIdAsync(Guid roleId);
        Task EditByViewModelAsync(RoleEditModel model);
        Task<Role> FindByUserIdAsync(Guid userId);
        Task<IList<string>> GetRoleNamesByUserIdAsync(Guid userId);
        Task<string> GenerateCodeAsync();
        Task<IList<SelectList>> GetRolesAsSelectListAsync();
        Task<IList<Role>> GetRolesByRequestAsync(RoleSearchModel model);
        Task<IList<Role>> GetRolesByUserIdAsync(Guid userId);
        Task<bool> IsExistNameAsync(string name, CancellationToken cancellationToken = default (CancellationToken));
        Task<string> MaxByRequestAsync(RoleSearchModel model, string aggregateMember);
        IQueryable<Role> QueryByRequest(RoleSearchModel model);
        Task<Role> FindAsync(Guid roleId);
        Task<IList<string>> GetPermissionNamesByUserIdAsync(Guid userId);
        Task<IList<Role>> GetRolesByPermissionIdAsync(Guid permissionId);
        Task<Role> FindByNameAsync(string roleName);
    }
}