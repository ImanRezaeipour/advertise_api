using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Announces
{
    public class AnnounceOptionListModel : BaseModel
    {
        public IEnumerable<AnnounceOptionModel> AnnounceOptions { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public AnnounceOptionSearchModel SearchRequest { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}