using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Categories
{
    public class CategoryReviewCreateModel : BaseModel
    {
        public string Body { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsActive { get; set; }
        public string Title { get; set; }
    }
}