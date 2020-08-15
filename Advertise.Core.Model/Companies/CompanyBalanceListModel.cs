using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyBalanceListModel : BaseModel
    {
        public IEnumerable<CompanyBalanceViewModel> CompanyBalances { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public CompanyBalanceSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}