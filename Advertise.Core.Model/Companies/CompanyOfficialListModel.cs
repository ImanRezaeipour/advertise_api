using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyOfficialListModel : BaseModel
    {
        public IEnumerable<CompanyOfficialModel> CompanyOfficials { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public CompanyOfficialSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<SelectList> StateList { get; set; }
    }
}