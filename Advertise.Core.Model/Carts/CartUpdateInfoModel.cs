using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Locations;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Carts
{
    public class CartUpdateInfoModel : BaseModel
    {
        public LocationModel Location { get; set; }
        public string AddressCity { get; set; }
        public string AddressProvince { get; set; }
        public Guid CityId { get; set; }
        public Guid CreatedById { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public PaymentType Payment { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public Guid ProvinceId { get; set; }
        public Guid ReceiptId { get; set; }
        public string TransfereeName { get; set; }
        public string UserAddress { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserNationalCode { get; set; }
    }
}