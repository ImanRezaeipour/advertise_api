using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyListModel : BaseModel
    {
        public IEnumerable<CompanyModel> Companies { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public CompanySearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<SelectList> StateList { get; set; }
    }
}