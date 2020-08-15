using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Categories
{
    public class CategoryOptionModel : BaseModel
    {
        public string Companies { get; set; }
        public string CompanyInfo { get; set; }
        public string CompanyManagement { get; set; }
        public Guid Id { get; set; }
        public string MyCompany { get; set; }
        public string ProductDescription { get; set; }
        public string Products { get; set; }
        public string ProductsManagement { get; set; }
        public string Title { get; set; }
        public bool? HasPrice { get; set; }
    }
}