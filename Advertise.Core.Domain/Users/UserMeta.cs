using System;
using System.ComponentModel.DataAnnotations.Schema;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Locations;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Users
{
    public class UserMeta : BaseEntity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string NationalCode { get; set; }
        public virtual DateTime? BirthOn { get; set; }
        public virtual DateTime? MarriedOn { get; set; }
        public virtual string AvatarFileName { get; set; }
        public virtual bool? IsActive { get; set; }
        public virtual GenderType? Gender { get; set; }
        public virtual string AboutMe { get; set; }
        public virtual string HomeNumber { get; set; }
        public virtual string PhoneNumber { get; set; }
        [NotMapped]
        public virtual string FullName => FirstName + " " + LastName;
        public virtual Location Location { get; set; }
        public virtual Guid? LocationId { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}