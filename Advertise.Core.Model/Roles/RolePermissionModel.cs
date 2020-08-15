using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Roles
{
    public class RolePermissionModel : BaseModel
    {
        public virtual Guid PermissionId { get; set; }
        public virtual Guid RoleId { get; set; }
    }
}