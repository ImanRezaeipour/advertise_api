using System;

namespace Advertise.Core.Model.Products
{
    public class ProductSpecificationDetailModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid SpecificationId { get; set; }
        public string Value { get; set; }
    }
}