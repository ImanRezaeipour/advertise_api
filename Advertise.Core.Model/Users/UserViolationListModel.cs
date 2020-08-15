using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Users
{
    public class UserViolationListModel : BaseModel
    {
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public IEnumerable<SelectList> ProductList { get; set; }
        public IEnumerable<SelectList> ReadList { get; set; }
        public IEnumerable<SelectList> ReasonTypeList { get; set; }
        public IEnumerable<UserViolationModel> UserViolations { get; set; }
        public UserViolationSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<SelectList> UserList { get; set; }
    }
}