using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Notices
{
    public class NoticeModel : BaseModel
    {
        public string Body { get; set; }
        public DateTime? ExpiredOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string Title { get; set; }
    }
}