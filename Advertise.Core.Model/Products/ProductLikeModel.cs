using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Users;

namespace Advertise.Core.Model.Products
{
    public class ProductLikeModel : BaseModel
    {
        public string BrandName { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid Id { get; set; }
        public bool IsFollow { get; set; }
        public UserModel LikedBy { get; set; }
        public string LogoFileName { get; set; }
        public string PhoneNumber { get; set; }
        public ProductModel Product { get; set; }
        public UserModel User { get; set; }
    }
}