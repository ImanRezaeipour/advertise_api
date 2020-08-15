using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Announces
{
    public class AnnouncePaymentCallbackModel : BaseModel
    {
        public string Authority { get; set; }
        public string Request { get; set; }
        public string Status { get; set; }
    }
}