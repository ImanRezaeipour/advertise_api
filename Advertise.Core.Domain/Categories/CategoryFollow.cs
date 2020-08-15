using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Categories
{
    public class CategoryFollow : BaseFollow
    {
        public virtual User FollowedBy { get; set; }
        public virtual Guid? FollowedById { get; set; }
        public virtual Category Category { get; set; }
        public virtual Guid? CategoryId { get; set; }
    }
}