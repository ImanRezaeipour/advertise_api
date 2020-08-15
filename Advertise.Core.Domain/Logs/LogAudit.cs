using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Logs
{
    public class LogAudit : BaseEntity
    {
        public virtual string CreatorIp { get; set; }
        public virtual string ModifierIp { get; set; }
        public virtual bool? IsModifyLock { get; set; }
        public virtual DateTime? ModifiedOn { get; set; }
        public virtual string ModifierAgent { get; set; }
        public virtual string CreatorAgent { get; set; }
        public virtual AuditType? Audit { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime? OperatedOn { get; set; }
        public virtual string Entity { get; set; }
        public virtual string XmlOldValue { get; set; }
        [NotMapped]
        public virtual XElement XmlOldValueWrapper
        {
            get { return XElement.Parse(XmlOldValue); }
            set { XmlOldValue = value.ToString(); }
        }
        public virtual string XmlNewValue { get; set; }
        [NotMapped]
        public virtual XElement XmlNewValueWrapper
        {
            get { return XElement.Parse(XmlNewValue); }
            set { XmlNewValue = value.ToString(); }
        }
        public virtual string EntityId { get; set; }
        public virtual string Agent { get; set; }
        public virtual string OperantIp { get; set; }
        public virtual User OperantedBy { get; set; }
        public virtual Guid? OperantedById { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual Guid? ModifiedById { get; set; }
    }
}