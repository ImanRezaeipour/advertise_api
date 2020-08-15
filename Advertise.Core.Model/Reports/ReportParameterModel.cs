using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Reports
{
    public class ReportParameterModel : BaseModel
    {
        public Guid Id { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}