using System;
using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Announces
{
    public class AnnounceReserve:BaseEntity
    {
        public virtual DateTime? Day { get; set; }
        public virtual Announce Announce { get; set; }
        public virtual Guid? AnnounceId { get; set; }
    }
}