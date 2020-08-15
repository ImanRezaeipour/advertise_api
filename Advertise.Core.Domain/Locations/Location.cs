using System;
using System.Collections.Generic;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Domain.Receipts;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Locations
{
    public class Location : BaseEntity
    {
        public virtual string Latitude { get; set; }
        public virtual string Longitude { get; set; }
        public virtual string Street { get; set; }
        public virtual string Extra { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual ICollection<Receipt> Receipts { get; set; }
        public virtual LocationCity LocationCity { get; set; }
        public virtual Guid? CityId { get; set; }
        public virtual ICollection<UserMeta> Metas { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}