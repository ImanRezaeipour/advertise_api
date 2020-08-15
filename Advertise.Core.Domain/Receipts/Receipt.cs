using System;
using System.Collections.Generic;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Locations;
using Advertise.Core.Domain.Users;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Receipts
{
    public class Receipt : BaseEntity
    {
        public virtual bool? IsBuy { get; set; }
        public virtual PaymentType? Payment { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string NationalCode { get; set; }
        public virtual string TransfereeName { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string HomeNumber { get; set; }
        public virtual string Email { get; set; }
        public virtual decimal? TotalProductsPrice { get; set; }
        public virtual decimal? TransportationCost { get; set; }
        public virtual decimal? FinalPrice { get; set; }
        public virtual DateTime? ConfirmedOn { get; set; }
        public virtual string TrackingCode { get; set; }
        public virtual string InvoiceNumber { get; set; }
        public virtual Location Location { get; set; }
        public virtual Guid? LocationId { get; set; }
        public virtual ICollection<ReceiptOption> Options { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}