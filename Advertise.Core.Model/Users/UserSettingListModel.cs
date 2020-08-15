using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Users
{
    public class UserSettingListModel:BaseModel 
    {
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public UserSettingSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<UserSettingModel> UserSettings { get; set; }
    }
}