using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Carts
{
    public class CartModel : BaseModel
    {
        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string CompanyAlias { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyTitle { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid Id { get; set; }
        public string ProductCode { get; set; }
        public int ProductCount { get; set; }
        public string ProductDescription { get; set; }
        public Guid ProductId { get; set; }
        public string ProductImageFileName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductTitle { get; set; }
        public decimal TotalProductPrice { get; set; }
    }
}