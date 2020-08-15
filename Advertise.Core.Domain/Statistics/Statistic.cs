using System;
using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Statistics
{
    public class Statistic : BaseEntity
    {
        public virtual string ActionName { get; set; }
        public virtual string ControllerName { get; set; }
        public virtual string IpAddress { get; set; }
        public virtual string Latitude { get; set; }
        public virtual string Longitude { get; set; }
        public virtual string Referrer { get; set; }
        public virtual string UserAgent { get; set; }
        public virtual string UserOs { get; set; }
        public virtual DateTime? ViewedOn { get; set; }
    }
}