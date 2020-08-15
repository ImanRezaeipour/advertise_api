using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Specifications
{
    public class SpecificationListModel : BaseModel
    {
        public IEnumerable<SelectList> CategoryList { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public SpecificationSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<SpecificationModel> Specifications { get; set; }
    }
}