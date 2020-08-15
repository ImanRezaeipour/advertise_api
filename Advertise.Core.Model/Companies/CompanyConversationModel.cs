using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Users;

namespace Advertise.Core.Model.Companies
{
    public class CompanyConversationModel : BaseModel
    {
        public string AvatarFileName { get; set; }
        public string Body { get; set; }
        public UserModel CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CurrentUserId { get; set; }
        public Guid Id { get; set; }
        public bool? Read { get; set; }
        public UserModel ReceivedBy { get; set; }
    }
}