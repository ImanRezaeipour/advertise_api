using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Complaints
{
    public class Complaint : BaseEntity
    {
        public virtual string Body { get; set; }
        public virtual string Title { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}