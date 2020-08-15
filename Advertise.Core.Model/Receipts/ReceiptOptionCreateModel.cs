using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Receipts
{
    public class ReceiptOptionCreateModel : BaseModel
    {
        public string CategoryCode { get; set; }
        public string CategoryTitle { get; set; }
        public string Code { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyTitle { get; set; }
        public decimal? Count { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? PreviousPrice { get; set; }
        public decimal? Price { get; set; }
        public string Title { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}