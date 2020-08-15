using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Users
{
    public class UserNotificationSearchModel : BaseSearchModel
    {
        public virtual Guid? CreatedById { get; set; }
        public virtual string Title { get; set; }
    }
}