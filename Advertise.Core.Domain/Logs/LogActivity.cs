using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Logs
{
    public class LogActivity : BaseEntity
    {
        public virtual string Comment { get; set; }
        public virtual DateTime? OperatedOn { get; set; }
        public virtual string Url { get; set; }
        public virtual string Title { get; set; }
        public virtual string Agent { get; set; }
        public virtual string OperantIp { get; set; }
        public virtual User OperantedBy { get; set; }
        public virtual Guid? OperantedById { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}