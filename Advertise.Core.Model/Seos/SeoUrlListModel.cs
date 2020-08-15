using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Seos
{
    public class SeoUrlListModel : BaseModel
    {
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public SeoUrlSearchModel SearchModel { get; set; }
        public IEnumerable<SeoUrlModel> SeoUrls { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}