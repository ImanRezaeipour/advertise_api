using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Announces
{
    public class AnnounceOptionModel : BaseModel
    {
        public Guid Id { get; set; }
        public int? Order { get; set; }
        public AnnouncePositionType? PositionType { get; set; }
        public decimal? Price { get; set; }
        public string Title { get; set; }
        public AnnounceType? Type { get; set; }
        public DateTime? ReleaseTime { get; set; }
    }
}