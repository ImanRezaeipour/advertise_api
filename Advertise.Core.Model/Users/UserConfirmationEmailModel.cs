using System;

namespace Advertise.Core.Model.Users
{
    public class UserConfirmationEmailModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
    }
}