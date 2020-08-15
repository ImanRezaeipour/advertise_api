using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Seos
{
    public class SeoUrlModel : BaseModel
    {
        public string AbsoulateUrl { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CurrentUrl { get; set; }
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public RedirectionType Redirection { get; set; }
    }
}