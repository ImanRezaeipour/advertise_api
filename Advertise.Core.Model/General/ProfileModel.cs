using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.General
{
    public class ProfileModel : BaseModel
    {
        public int FollowersCount { get; set; }
        public bool IsSetCompanyAlias { get; set; }
        public bool IsSetUsername { get; set; }
        public int ProductApprovedCount { get; set; }
        public int ProductPendingCount { get; set; }
        public int ProductRejectCount { get; set; }
        public decimal ReceiptSum { get; set; }
        public Guid UserId { get; set; }
    }
}