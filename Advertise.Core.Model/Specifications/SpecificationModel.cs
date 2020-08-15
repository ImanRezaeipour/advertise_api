using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Specifications
{
    public class SpecificationModel : BaseModel
    {
        public string CategoryAlias { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryImageFileName { get; set; }
        public string CategoryMetaTitle { get; set; }
        public string CategoryTitle { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
        public IEnumerable<SelectList> DropDownListSpecificationOptions { get; set; }
        public IEnumerable<SpecificationOptionModel> Options { get; set; }
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public SpecificationType Type { get; set; }
    }
}