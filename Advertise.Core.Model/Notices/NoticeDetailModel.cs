using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Notices
{
    public class NoticeDetailModel : BaseModel
    {
        public DateTime? ExpiredOn { get; set; }
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
    }
}