using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyReviewListModel : BaseModel
    {
        public IEnumerable<SelectList> ActiveList { get; set; }
        public IEnumerable<SelectList> CompanyList { get; set; }
        public IEnumerable<CompanyReviewModel> CompanyReviews { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public CompanyReviewSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<SelectList> UserList { get; set; }
        public bool IsMyself { get; set; }
    }
}