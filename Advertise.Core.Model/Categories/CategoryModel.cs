using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Categories
{
    public class CategoryModel : BaseModel
    {
        public string Alias { get; set; }
        public CategoryOptionModel CategoryOption { get; set; }
        public string Code { get; set; }
        public int CompanyCount { get; set; }
        public string Description { get; set; }
        public bool? HasChild { get; set; }
        public string Icon { get; set; }
        public Guid Id { get; set; }
        public string ImageFileName { get; set; }
        public bool InitFollow { get; set; }
        public bool? IsActive { get; set; }
        public string MetaTitle { get; set; }
        public int Order { get; set; }
        public string ParentAlias { get; set; }
        public string ParentCode { get; set; }
        public Guid? ParentId { get; set; }
        public string ParentImageFileName { get; set; }
        public string ParentMetaTitle { get; set; }
        public string ParentTitle { get; set; }
        public int ProductCount { get; set; }
        public string Title { get; set; }
        public CategoryType? Type { get; set; }
    }
}