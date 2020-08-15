using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Emails
{
    public class Email : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
        public virtual bool? IsSend { get; set; }
        public virtual User SentBy { get; set; }
        public virtual Guid? SentById { get; set; }
        public virtual User RecievedBy { get; set; }
        public virtual Guid? RecievedById { get; set; }
    }
}