using Advertise.Core.Domain.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Announces
{
    public class AnnounceOption : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual int? Capacity { get; set; }
        public virtual decimal? Price { get; set; }
        public virtual AnnouncePositionType? PositionType { get; set; }
        public virtual AnnounceType? Type { get; set; }
    }
}