using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Complaints
{
    public class ComplaintDetailModel : BaseModel
    {
        public string Body { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}