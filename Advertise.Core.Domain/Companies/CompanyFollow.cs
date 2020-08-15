using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Companies
{
    public class CompanyFollow : BaseFollow
    {
        public virtual User FollowedBy { get; set; }
        public virtual Guid? FollowedById { get; set; }
        public virtual Company Company { get; set; }
        public virtual Guid? CompanyId { get; set; }
    }
}