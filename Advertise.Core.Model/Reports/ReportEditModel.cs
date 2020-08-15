using System;
using System.Web;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Reports
{
    public class ReportEditModel : BaseModel
    {
        public Guid Id { get; set; }
        public HttpPostedFileBase ContentFile { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }
}