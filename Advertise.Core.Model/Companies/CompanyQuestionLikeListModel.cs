using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanyQuestionLikeListModel : BaseModel
    {
        public IEnumerable<SelectList> CategoryList { get; set; }
        public IEnumerable<CompanyQuestionLikeModel> CompanyQuestionLikes { get; set; }
        public IEnumerable<SelectList> FollowList { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public CompanyQuestionLikeSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<SelectList> UserList { get; set; }
    }
}