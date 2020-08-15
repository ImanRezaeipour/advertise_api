using System.Collections.Generic;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Products;

namespace Advertise.Core.Domain.Categories
{
    public class CategoryOption : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string ProductText { get; set; }
        public virtual string ProductDescriptionText { get; set; }
        public virtual string ProductsManagementText { get; set; }
        public virtual string CompanyText { get; set; }
        public virtual string CompanyInfoText { get; set; }
        public virtual string CompanyManagementText { get; set; }
        public virtual string MyCompanyText { get; set; }
        public virtual bool? HasPrice { get; set; }
        public virtual string BackgroundFileName { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}