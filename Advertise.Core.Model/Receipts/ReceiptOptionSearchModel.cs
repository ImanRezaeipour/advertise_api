using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Receipts
{
    public class ReceiptOptionSearchModel : BaseSearchModel
    {
        public ReceiptOptionListType? ListType { get; set; }
        public Guid? ReceiptId { get; set; }
        public Guid? UserId { get; set; }
    }
}