using System;
using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Companies
{
    public class CompanyImageFile : BaseImage
    {
        public virtual int? Order { get; set; }
        public virtual bool? IsWatermark { get; set; }
        public virtual CompanyImage CompanyImage { get; set; }
        public virtual Guid? CompanyImageId { get; set; }
    }
}
