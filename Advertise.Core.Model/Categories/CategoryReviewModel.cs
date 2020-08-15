using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Categories
{
    public class CategoryReviewModel : BaseModel
    {
        public string AvatarFileName { get; set; }
        public string Body { get; set; }
        public string CategoryAlias { get; set; }
        public string CategoryFileName { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryImageFileName { get; set; }
        public string CategoryTitle { get; set; }
        public DateTime CreatedOn { get; set; }
        public string FullName { get; set; }
        public Guid Id { get; set; }
        public string ImageFileName { get; set; }
        public bool IsActive { get; set; }
        public string RejectDescription { get; set; }
        public StateType State { get; set; }
        public string Title { get; set; }
        public string TitleCategory { get; set; }
        public Guid UserId { get; set; }
    }
}