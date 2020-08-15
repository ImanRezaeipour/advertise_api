using System;
using System.Collections.Generic;
using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Locations
{
    public class LocationCity : BaseEntity
    {
        public virtual bool? IsState { get; set; }
        public virtual string Name { get; set; }
        public virtual bool? IsActive { get; set; }
        public virtual string Latitude { get; set; }
        public virtual string Longitude { get; set; }
        public virtual LocationCity Parent { get; set; }
        public virtual Guid? ParentId { get; set; }
        public virtual ICollection<LocationCity> Childrens { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}