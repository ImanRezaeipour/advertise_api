using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Locations
{
    public class LocationCreateModel : BaseModel
    {
        public LocationCityModel LocationCity { get; set; }
        public string Extra { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
    }
}