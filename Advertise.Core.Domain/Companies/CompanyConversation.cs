using System;
using System.Collections.Generic;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Companies
{
    public class CompanyConversation : BaseEntity
    {
        public virtual string Body { get; set; }
        public virtual bool? IsRead { get; set; }
        public virtual bool? IsDeletedBySender { get; set; }
        public virtual bool? IsDeletedByReceiver { get; set; }
        public virtual User ReceivedBy { get; set; }
        public virtual Guid? ReceivedById { get; set; }
        public virtual CompanyConversation Reply { get; set; }
        public virtual Guid? ReplyId { get; set; }
        public virtual ICollection<CompanyConversation> Childrens { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}