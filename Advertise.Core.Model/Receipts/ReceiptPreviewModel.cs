using System;
using System.Collections.Generic;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Locations;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Receipts
{
    public class ReceiptPreviewModel : BaseModel
    {
        public LocationModel Location { get; set; }
        public Guid CreatedById { get; set; }
        public string CurrentDate { get; set; }
        public decimal DiscountCodePrice { get; set; }
        public string Email { get; set; }
        public decimal FinalPrice { get; set; }
        public string FirstName { get; set; }
        public string HomeNumber { get; set; }
        public Guid Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public PaymentType Payment { get; set; }
        public string PhoneNumber { get; set; }
        public string ProvinceName { get; set; }
        public IEnumerable<ReceiptOptionModel> ReceiptOptions { get; set; }
        public decimal TotalProductsPrice { get; set; }
        public string TrackingCode { get; set; }
        public string TransfereeName { get; set; }
        public decimal TransportationCost { get; set; }
    }
}