using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Locations
{
    public class LocationEditModel : BaseModel
    {
        public IEnumerable<SelectList> Cities { get; set; }
        public Guid LocationCityId { get; set; }
        public string Extra { get; set; }
        public Guid Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public Guid UserId { get; set; }
    }
}