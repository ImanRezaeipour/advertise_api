using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Statistics
{
    public class StatisticCreateModel : BaseModel
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string IpAddress { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Referrer { get; set; }
        public string UserAgent { get; set; }
        public string UserOs { get; set; }
        public DateTime ViewedOn { get; set; }
    }
}