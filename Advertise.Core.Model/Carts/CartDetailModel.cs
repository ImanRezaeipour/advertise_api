using System;
using System.Collections.Generic;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Carts
{
    public class CartDetailModel : BaseModel
    {
        public IEnumerable<CartModel> Carts { get; set; }
        public Guid Id { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserDisplayName { get; set; }
        public string UserFullName { get; set; }
        public Guid UserId { get; set; }
    }
}