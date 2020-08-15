using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Smses
{
    public class SmsOperator : BaseEntity
    {
        public virtual bool IsActive { get; set; }
        public virtual bool IsAllowExecuteCommand { get; set; }
        public virtual bool IsAllowReadCommand { get; set; }
        public virtual string MobileNumber { get; set; }
    }
}