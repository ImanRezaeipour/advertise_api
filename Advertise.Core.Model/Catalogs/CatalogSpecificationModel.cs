using System;
using System.Collections.Generic;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Specifications;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Catalogs
{
    public class CatalogSpecificationModel : BaseModel
    {
        public Guid? Id { get; set; }
        public Guid? CatalogId { get; set; }
        public Guid? SpecificationId { get; set; }
        public Guid? SpecificationOptionId { get; set; }
        public string Value { get; set; }
        public IEnumerable<SpecificationOptionModel> Options { get; set; }
        public int Order { get; set; }
        public IEnumerable<Guid?> SpecificationOptionIdList { get; set; }
        public string Title { get; set; }
        public SpecificationType Type { get; set; }
    }
}