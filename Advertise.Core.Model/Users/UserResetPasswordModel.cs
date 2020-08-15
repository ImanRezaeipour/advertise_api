using System;

namespace Advertise.Core.Model.Users
{
    public class UserResetPasswordModel
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid UserId { get; set; }
    }
}