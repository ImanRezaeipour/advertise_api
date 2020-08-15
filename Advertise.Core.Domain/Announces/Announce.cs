using System;
using System.Collections.Generic;
using Advertise.Core.Domain.Categories;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Announces
{
    public class Announce : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string ImageFileName { get; set; }
        public virtual string EntityName { get; set; }
        public virtual Guid? EntityId { get; set; }
        public virtual Guid? TargetId { get; set; }
        public virtual bool? IsApprove { get; set; }
        public virtual DurationType? DurationType { get; set; }
        public virtual decimal? FinalPrice { get; set; }
        public virtual int? Order { get; set; }
        public virtual User Owner { get; set; }
        public virtual Guid? OwnerId { get; set; }
        public virtual AnnounceOption AnnounceOption { get; set; }
        public virtual Guid? AnnounceOptionId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Guid? CategoryId { get; set; }
        public virtual ICollection<AnnouncePayment> Payments { get; set; }
        public virtual ICollection<AnnounceReserve> Reserves { get; set; }
    }
}