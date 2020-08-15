using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Plans
{
    public class PlanPaymentListModel : BaseModel
    {
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public IEnumerable<PlanPaymentModel> PlanPayments { get; set; }
        public PlanPaymentSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}