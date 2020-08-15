using System;
using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Notices
{
    public class Notice : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
        public virtual bool? IsActive { get; set; }
        public virtual DateTime? ExpiredOn { get; set; }
      }
}