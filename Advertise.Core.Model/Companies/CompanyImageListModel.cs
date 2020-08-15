using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyImageListModel : BaseModel
    {
        public IEnumerable<CompanyImageModel> CompanyImages { get; set; }
        public IEnumerable<SelectList> CompanyList { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public CompanyImageSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<SelectList> StateList { get; set; }
        public IEnumerable<SelectList> UserList { get; set; }
    }
}