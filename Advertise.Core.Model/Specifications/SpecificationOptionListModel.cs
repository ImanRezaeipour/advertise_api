using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Specifications
{
    public class SpecificationOptionListModel : BaseModel
    {
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public SpecificationOptionSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<SelectList> SpecificationList { get; set; }
        public IEnumerable<SpecificationOptionModel> SpecificationOptions { get; set; }
    }
}