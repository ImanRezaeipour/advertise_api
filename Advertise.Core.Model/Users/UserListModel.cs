using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Users
{
    public class UserListModel : BaseModel
    {
        public IEnumerable<SelectList> GenderList { get; set; }
        public IEnumerable<SelectList> IsActiveList { get; set; }
        public IEnumerable<SelectList> IsBanList { get; set; }
        public IEnumerable<SelectList> IsVerifyList { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public UserSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<UserModel> UserList { get; set; }
        public IEnumerable<UserModel> Users { get; set; }
    }
}