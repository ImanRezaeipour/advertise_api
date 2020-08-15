using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;

namespace Advertise.Core.Domain.Categories
{
    public class CategoryReview : BaseReview
    {
        public virtual Category Category { get; set; }
        public virtual Guid? CategoryId { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}