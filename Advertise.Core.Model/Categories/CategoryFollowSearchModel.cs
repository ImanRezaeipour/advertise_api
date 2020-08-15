using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Categories
{
    public class CategoryFollowSearchModel : BaseSearchModel
    {
        public Guid? CategoryId { get; set; }
        public Guid? FollowedById { get; set; }
        public bool? IsFollow { get; set; }
    }
}