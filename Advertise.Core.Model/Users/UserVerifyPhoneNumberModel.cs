namespace Advertise.Core.Model.Users
{
    public class UserVerifyPhoneNumberModel
    {
        public string Code { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}