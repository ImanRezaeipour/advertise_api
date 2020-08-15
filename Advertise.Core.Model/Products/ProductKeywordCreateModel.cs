using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductKeywordCreateModel : BaseModel
    {
        public Guid? KeywordId { get; set; }
        public Guid? ProductId { get; set; }
        public string Title { get; set; }
    }
}