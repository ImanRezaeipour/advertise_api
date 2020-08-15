using System;
using System.Collections.Generic;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Companies
{
    public class CompanyAttachment : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual int? Order { get; set; }
        public virtual AttachmentType? Type { get; set; }
        public virtual StateType? State { get; set; }
        public virtual string RejectDescription { get; set; }
        public virtual Company Company { get; set; }
        public virtual Guid? CompanyId { get; set; }
        public virtual User ApprovedBy { get; set; }
        public virtual Guid? ApprovedById { get; set; }
        public virtual ICollection<CompanyAttachmentFile> AttachmentFiles { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}