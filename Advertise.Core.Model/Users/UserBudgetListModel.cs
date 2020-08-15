using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Users
{
    public class UserBudgetListModel
    {
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public UserBudgetSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<UserBudgetModel> UserBudgets { get; set; }
    }
}