using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Receipts
{
    public class ReceiptPaymentSearchModel : BaseSearchModel
    {
        public string AuthorityCode { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public StateType? State { get; set; }
    }
}