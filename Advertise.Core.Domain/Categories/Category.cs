using System;
using System.Collections.Generic;
using Advertise.Core.Domain.Announces;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Domain.Products;
using Advertise.Core.Domain.Users;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Categories
{
    public class Category : BaseEntity
    {
        public virtual string Code { get; set; }
        public virtual string Alias { get; set; }
        public virtual int Order { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string ImageFileName { get; set; }
        public virtual bool? HasChild { get; set; }
        public virtual bool? IsActive { get; set; }
        public virtual CategoryType? Type { get; set; }
        public virtual string MetaTitle { get; set; }
        public virtual string MetaKeywords { get; set; }
        public virtual string MetaDescription { get; set; }
        public virtual int? Level { get; set; }
        public virtual Category Parent { get; set; }
        public virtual Guid? ParentId { get; set; }
        public virtual CategoryOption CategoryOption { get; set; }
        public virtual Guid? CategoryOptionId { get; set; }
        public virtual ICollection<Category> Childrens { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<CategoryFollow> Follows { get; set; }
        public virtual ICollection<CategoryReview> Reviews { get; set; }
        public virtual ICollection<Announce> Announce { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}