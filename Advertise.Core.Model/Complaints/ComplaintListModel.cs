using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Complaints
{
    public class ComplaintListModel : BaseModel
    {
        public IEnumerable<ComplaintModel> Complaints { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public ComplaintSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}