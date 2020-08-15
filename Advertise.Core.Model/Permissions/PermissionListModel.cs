using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Permissions
{
    public class PermissionListModel : BaseModel
    {
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public IEnumerable<PermissionModel> Permissions { get; set; }
        public PermissionSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}