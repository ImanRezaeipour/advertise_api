using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Locations
{
    public class LocationCitySearchModel : BaseSearchModel
    {
        public DateTime? CreatedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsState { get; set; }
        public Guid? ParentId { get; set; }
        public string Title { get; set; }
        public Guid? UserId { get; set; }
    }
}