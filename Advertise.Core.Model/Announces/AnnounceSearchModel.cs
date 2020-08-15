using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Announces
{
    public class AnnounceSearchModel : BaseSearchModel
    {
        public bool? IsApprove { get; set; }
        public Guid? OwnerId { get; set; }
    }
}