using System.Collections.Generic;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Core.Model.Products;

namespace Advertise.Core.Model.General
{
    public class LandingPageModel : BaseModel
    {
        public IEnumerable<ProductModel> LastProductItemList { get; set; }
        public IEnumerable<ProductModel> LastMobileProductItemList { get; set; }
        public IEnumerable<CompanyModel> LastMobileCompanyItemList { get; set; }
        public IEnumerable<ProductModel> MostLikedItemList { get; set; }
        public IEnumerable<ProductModel> MostVisitedItemList { get; set; }
        public IEnumerable<ProductModel> MyLastVisitItemList { get; set; }
        public IEnumerable<CompanyModel> LastCompanyItemList { get; set; }
    }
}