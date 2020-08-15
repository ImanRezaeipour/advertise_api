using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductImageModel : BaseModel
    {
        public string CompanyCode { get; set; }
        public string CompanyTitle { get; set; }
        public string FileDimension { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public Guid Id { get; set; }
        public bool IsWatermark { get; set; }
        public int Order { get; set; }
        public string ProductCode { get; set; }
        public Guid ProductId { get; set; }
        public string ProductImageFileName { get; set; }
        public string ProductTitle { get; set; }
        public string Title { get; set; }
    }
}