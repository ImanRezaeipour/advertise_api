using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Seos
{
    public class SeoUrl : BaseEntity
    {
        public string AbsoulateUrl { get; set; }
        public string CurrentUrl { get; set; }
        public bool? IsActive { get; set; }
        public RedirectionType? Redirection { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}