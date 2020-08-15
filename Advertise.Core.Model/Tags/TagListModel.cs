using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Tags
{
    public class TagListModel :BaseModel
    {
        public IEnumerable<SelectList> ActiveList { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public TagSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
        public IEnumerable<TagModel> Tags { get; set; }
    }
}