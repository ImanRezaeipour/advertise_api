using System;
using System.Collections.Generic;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Roles;

namespace Advertise.Core.Domain.Permissions
{
    public class Permission : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string Name { get; set; }
        public virtual string MethodName { get; set; }
        public virtual string Description { get; set; }
        public virtual int? Order { get; set; }
        public virtual bool? IsPermission { get; set; }
        public virtual bool? IsCallback { get; set; }
        public virtual Permission Parent { get; set; }
        public virtual Guid? ParentId { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
