using Advertise.Core.Types;

namespace Advertise.Core.Domain.Common
{
    public class BaseRate : BaseEntity
    {
        public virtual bool? IsApprove { get; set; }
        public virtual RateType? Rate { get; set; }
    }
}