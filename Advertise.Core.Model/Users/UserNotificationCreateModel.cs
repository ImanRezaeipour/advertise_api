using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Users
{
    public class UserNotificationCreateModel : BaseModel
    {
        public bool IsRead { get; set; }
        public Guid TargetId { get; set; }
        public string TargetImage { get; set; }
        public string TargetTitle { get; set; }
        public NotificationType Type { get; set; }
    }
}