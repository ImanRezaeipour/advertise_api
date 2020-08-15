using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Smses
{
    public class SmsOperatorModel : BaseModel
    {
        public bool IsActive { get; set; }
        public bool IsAllowExecuteCommand { get; set; }
        public bool IsAllowReadCommand { get; set; }
        public string MobileNumber { get; set; }
    }
}