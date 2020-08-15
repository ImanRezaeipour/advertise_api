using System;
using System.Collections.Generic;
using System.Linq;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Carts
{
    public class CartListModel : BaseModel
    {
        public IEnumerable<CartModel> Carts { get; set; }
        public Guid Id { get; set; }
        public CartSearchModel SearchRequest { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalProductListPrice
        {
            get
            {
                return Carts.Sum(bag => bag.TotalProductPrice);
            }
        }
        public string UserDisplayName { get; set; }
        public string UserFullName { get; set; }
        public Guid UserId { get; set; }
    }
}