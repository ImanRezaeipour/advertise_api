using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Smses
{
    public class SmsOperatorEditModel : BaseModel
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsAllowExecuteCommand { get; set; }
        public bool IsAllowReadCommand { get; set; }
        public string MobileNumber { get; set; }
    }
}