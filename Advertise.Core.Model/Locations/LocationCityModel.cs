using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Locations
{
    public class LocationCityModel : BaseModel
    {
        public Guid Id { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsState { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Name { get; set; }
        public Guid ParentId { get; set; }
    }
}