using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Announces
{
    public class AnnouncePaymentListModel : BaseModel
    {
        public IEnumerable<AnnouncePaymentModel> Announces { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public AnnouncePaymentSearchModel SearchRequest { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}