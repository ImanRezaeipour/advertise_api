using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Announces
{
   public class AnnouncePaymentSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public bool? IsApprove { get; set; }
    }
}
