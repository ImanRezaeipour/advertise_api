using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Receipts
{
    public class ReceiptListModel : BaseModel
    {
        public IEnumerable<ReceiptModel> Receipts { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public ReceiptSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}