using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Advertise.Core.Domain.Users;
using Advertise.Core.Types;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Advertise.Core.Domain.Roles
{
    public class Role : IdentityRole<Guid, UserRole>
    {
        public virtual string Code { get; set; }
        [Timestamp]
        public virtual byte?[] RowVersion { get; set; }
        public virtual bool? IsSystemRole { get; set; }
        public virtual bool? IsBan { get; set; }
        [Column(TypeName = "xml")]
        public virtual string Permissions { get; set; }
        [NotMapped]
        public virtual XElement XmlPermissions
        {
            get { return XElement.Parse(Permissions); }
            set { Permissions = value.ToString(); }
        }
        public virtual DateTime? CreatedOn { get; set; }
        public virtual DateTime? ModifiedOn { get; set; }
        public virtual string CreatorIp { get; set; }
        public virtual string ModifierIp { get; set; }
        public virtual bool? IsModifyLock { get; set; }
        public virtual bool? IsDelete { get; set; }
        public virtual string ModifierAgent { get; set; }
        public virtual string CreatorAgent { get; set; }
        public virtual int? Version { get; set; }
        public virtual AuditType? Audit { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}