using System;
using System.Collections.Generic;

namespace Advertise.Core.Model.Products
{
    public class ProductSpecificationModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid SpecificationId { get; set; }
        public string SpecificationOptionTitle { get; set; }
        public int? SpecificationOrder { get; set; }
        public string SpecificationTitle { get; set; }
        public IEnumerable<string> SpecificationValues { get; set; }
        public string Value { get; set; }
    }
}