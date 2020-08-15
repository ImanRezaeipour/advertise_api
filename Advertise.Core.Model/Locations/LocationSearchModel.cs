using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Locations
{
    public class LocationSearchModel : BaseSearchModel
    {
        public DateTime? CreatedOn { get; set; }
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
    }
}