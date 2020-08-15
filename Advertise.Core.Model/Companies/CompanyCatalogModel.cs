using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Companies
{
    public class CompanyCatalogModel : BaseModel
    {
        public decimal Price { get; set; }
        public decimal? PreviousPrice { get; set; }
        public decimal? DiscountPercent { get; set; }
        public SellType? Sell { get; set; }
    }
}