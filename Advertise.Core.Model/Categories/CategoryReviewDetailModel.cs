using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Categories
{
    public class CategoryReviewDetailModel : BaseModel
    {
        public string Body { get; set; }
        public Guid CategoryId { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}