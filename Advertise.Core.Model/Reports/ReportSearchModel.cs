using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Reports
{
    public class ReportSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
    }
}