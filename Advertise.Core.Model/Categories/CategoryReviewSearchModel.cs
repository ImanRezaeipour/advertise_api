using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Categories
{
    public class CategoryReviewSearchModel : BaseSearchModel
    {
        public Guid? CategoryId { get; set; }
        public Guid? CreatedById { get; set; }
        public bool? IsActive { get; set; }
    }
}