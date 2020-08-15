using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Roles
{
    public class RoleListModel : BaseModel
    {
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public IEnumerable<RoleModel> Roles { get; set; }
        public RoleSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}