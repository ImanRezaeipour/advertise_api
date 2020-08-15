using System;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Users
{
    public class UserNotificationModel
    {
        public DateTime CreatedOn { get; set; }
        public Guid Id { get; set; }
        public bool IsRead { get; set; }
        public string Message { get; set; }
        public Guid OwnerId { get; set; }
        public Guid TargetId { get; set; }
        public string Code { get; set; }
        public string TargetImage { get; set; }
        public string TargetTitle { get; set; }
        public NotificationType Type { get; set; }
        public string Url { get; set; }
    }
}