using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Permissions;

namespace Advertise.Core.Domain.Roles
{
    public class RolePermission : BaseEntity
    {
        public virtual Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual Guid PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
