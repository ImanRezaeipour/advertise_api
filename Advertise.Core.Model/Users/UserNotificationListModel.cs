using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Users
{
    public class UserNotificationListModel : BaseModel
    {
        public IEnumerable<UserNotificationModel> UserNotifications { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public UserNotificationSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}