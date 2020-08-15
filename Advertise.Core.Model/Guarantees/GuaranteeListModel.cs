using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Guarantees
{
    public class GuaranteeListModel : BaseModel
    {
        public IEnumerable<GuaranteeModel> Guarantees { get;set;}
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public GuaranteeSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}