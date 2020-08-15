using System;
using System.Collections.Generic;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Categories
{
    public class CategoryDetailModel : BaseModel
    {
        public string Alias { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }
        public CategoryOptionModel CategoryOption { get; set; }
        public CategoryType? CategoryType { get; set; }
        public CategoryModel Childs { get; set; }
        public int CompanyCount { get; set; }
        public string Description { get; set; }
        public int FollowerCount { get; set; }
        public Guid Id { get; set; }
        public string ImageFileName { get; set; }
        public bool InitFollow { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaTitle { get; set; }
        public int ProductCount { get; set; }
        public string Title { get; set; }
        public int VisitCount { get; set; }
    }
}