using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Locations
{
    public class LocationListModel : BaseModel
    {
        public IEnumerable<LocationModel> Locations { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public LocationSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}