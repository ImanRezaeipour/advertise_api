using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyFollowListModel : BaseModel
    {
        public IEnumerable<SelectList> CategoryList { get; set; }
        public IEnumerable<CompanyFollowModel> CompanyFollows { get; set; }
        public IEnumerable<SelectList> FollowList { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public CompanyFollowSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> UserList { get; set; }
    }
}