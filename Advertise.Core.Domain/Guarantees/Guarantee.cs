using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Guarantees
{
    public class Guarantee : BaseEntity
    {
        public virtual string Description { get; set; }
        public virtual string Email { get; set; }
        public virtual string MobileNumber { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Title { get; set; }
    }
}