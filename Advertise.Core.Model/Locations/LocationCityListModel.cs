using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Locations
{
    public class LocationCityListModel : BaseModel
    {
        public IEnumerable<LocationCityModel> LocationCities { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public LocationCitySearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}