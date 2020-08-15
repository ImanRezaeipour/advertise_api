using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanySocialListModel : BaseModel
    {
        public IEnumerable<SelectList> CompanyList { get; set; }
        public IEnumerable<CompanySocialModel> CompanySocials { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public CompanySocialSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<SelectList> UserList { get; set; }
    }
}