using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Keywords;

namespace Advertise.Core.Domain.Products
{
    public class ProductKeyword : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual Keyword Keyword { get; set; }
        public virtual Guid? KeywordId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Guid? ProductId { get; set; }
    }
}