using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Smses
{
    public class SmsSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public Guid? ProductId { get; set; }
    }
}