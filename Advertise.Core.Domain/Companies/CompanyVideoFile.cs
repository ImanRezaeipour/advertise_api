using System;
using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Companies
{
    public class CompanyVideoFile : BaseVideo
    {
        public virtual int? Order { get; set; }
        public virtual bool? IsWatermark { get; set; }
        public virtual string WatermarkName { get; set; }
        public virtual string ThumbName { get; set; }
        public virtual CompanyVideo CompanyVideo { get; set; }
        public virtual Guid? CompanyVideoId { get; set; }
    }
}
