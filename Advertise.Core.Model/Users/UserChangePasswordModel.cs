using System;

namespace Advertise.Core.Model.Users
{
    public class UserChangePasswordModel
    {
        public string ConfirmPassword { get; set; }
        public Guid Id { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}