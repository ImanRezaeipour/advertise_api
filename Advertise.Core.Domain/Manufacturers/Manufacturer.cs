using Advertise.Core.Domain.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Manufacturers
{
    public class Manufacturer : BaseEntity
    {
        public virtual CountryType? Country { get; set; }
        public virtual string Name { get; set; }
    }
}