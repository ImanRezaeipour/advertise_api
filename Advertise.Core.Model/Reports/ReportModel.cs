using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Reports
{
    public class ReportModel : BaseModel
    {
        public Guid? Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string ParentId { get; set; }
        public bool? HasChild { get; set; }
    }
}