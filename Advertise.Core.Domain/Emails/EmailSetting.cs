namespace Advertise.Core.Domain.Emails
{
    public class EmailSetting
    {
        public virtual string DeliveryMethod { get; set; }
        public virtual string From { get; set; }
        public virtual string Host { get; set; }
        public virtual string Port { get; set; }
        public virtual bool DefaultCredentials { get; set; }
        public virtual string ClientDomain { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual bool EnableSsl { get; set; }
    }
}