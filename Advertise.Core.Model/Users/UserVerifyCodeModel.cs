namespace Advertise.Core.Model.Users
{
    public class UserVerifyCodeModel
    {
        public string Code { get; set; }
        public string Provider { get; set; }
        public bool RememberBrowser { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}