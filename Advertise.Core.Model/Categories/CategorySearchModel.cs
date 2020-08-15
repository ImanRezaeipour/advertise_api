using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Categories
{
    public class CategorySearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public Guid? Id { get; set; }
        public bool? IsActive { get; set; }
        public Guid? ParentId { get; set; }
        public CategoryType? Type { get; set; }
        public bool? WithMany { get; set; }
        public bool? WithRoot { get; set; }
    }
}