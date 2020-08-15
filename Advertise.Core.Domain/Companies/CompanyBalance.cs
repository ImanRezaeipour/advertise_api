using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Settings;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Companies
{
    public class CompanyBalance : BaseEntity
    {
        public virtual int? Amount { get; set; }
        public virtual DateTime? TransactionedOn { get; set; }
        public virtual string Description { get; set; }
        public virtual string IssueTracking { get; set; }
        public virtual string DocumentNumber { get; set; }
        public virtual string Depositor { get; set; }
        public virtual Company Company { get; set; }
        public virtual Guid? CompanyId { get; set; }
        public virtual  SettingTransaction SettingTransaction { get; set; }
        public virtual  Guid? SettingTransactionId { get; set; }
        public virtual string AttachmentFile { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}
