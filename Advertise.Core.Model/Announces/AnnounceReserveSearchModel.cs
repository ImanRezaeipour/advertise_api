using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Announces
{
   public class AnnounceReserveSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public bool? IsApprove { get; set; }
        public DateTime? Day { get; set; }
        public Guid? OptionId { get; set; }
    }
}
