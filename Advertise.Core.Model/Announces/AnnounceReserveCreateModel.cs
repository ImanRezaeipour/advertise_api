using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Announces
{
    public class AnnounceReserveCreateModel : BaseModel
    {
        public Guid? AdsId { get; set; }
        public DateTime? Day { get; set; }
        public DurationType DurationType { get; set; }
        public Guid? AdsOptionId { get; set; }
    }
}