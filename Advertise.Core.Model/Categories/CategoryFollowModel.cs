using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Categories
{
    public class CategoryFollowModel : BaseModel
    {
        public string CategoryAlias { get; set; }
        public int CategoryCompanyCount { get; set; }
        public Guid CategoryId { get; set; }
        public Guid Id { get; set; }
        public string CategoryImageFileName { get; set; }
        public string CategoryMetaTitle { get; set; }
        public int CategoryOrder { get; set; }
        public int CategoryProductCount { get; set; }
        public string CategoryTitle { get; set; }
        public bool InitFollowCategory { get; set; }
        public string ParentCategoryAlias { get; set; }
        public string ParentCategoryImageFileName { get; set; }
        public string ParentCategoryMetaTitle { get; set; }
        public string ParentCategoryTitle { get; set; }
    }
}