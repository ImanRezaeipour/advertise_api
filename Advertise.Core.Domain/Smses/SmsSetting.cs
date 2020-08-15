namespace Advertise.Core.Domain.Smses
{
    public class SmsSetting
    {
        public virtual bool IsEnabled { get; set; }
        public virtual string ApiKey { get; set; }
        public virtual string SecretKet { get; set; }
        public virtual string LineNumber { get; set; }
    }
}