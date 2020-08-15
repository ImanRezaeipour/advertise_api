using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Specifications
{
    public class SpecificationOptionModel : BaseModel
    {
        public string CategoryAlias { get; set; }
        public string CategoryImageFileName { get; set; }
        public string CategoryMetaTitle { get; set; }
        public string CategoryTitle { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
        public int Order { get; set; }
        public Guid SpecificationId { get; set; }
        public string SpecificationTitle { get; set; }
        public string Title { get; set; }
        public bool? IsSelected { get; set; }
    }
}