using System.Collections.Generic;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Categories
{
    public class CategoryBreadCrumbModel : BaseModel
    {
        public string CurrentTitle { get; set; }
        public bool? IsAllSearch { get; set; }
        public IEnumerable<CategoryModel> Nodes { get; set; }
    }
}