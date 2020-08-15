using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Categories
{
    public class CategoryCreateModel : BaseModel
    {
        public string Alias { get; set; }
        public IEnumerable<SelectList> CategoryOptionList { get; set; }
        public string Description { get; set; }
        public string ImageFileName { get; set; }
        public string MetaTitle { get; set; }
        public int Order { get; set; }
        public Guid? ParentId { get; set; }
        public string Title { get; set; }
    }
}