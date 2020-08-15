using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Carts
{
    public class CartSearchModel : BaseSearchModel
    {
        public Guid? CreatedById { get; set; }
        public bool? IsActive { get; set; }
    }
}