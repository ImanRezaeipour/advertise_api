using System.Collections.Generic;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Products
{
    public class ProductQueryStringModel : BaseModel
    {
        public string QueryString { get; set; }
        public Dictionary<string,string> DictionaryQueryString { get; set; }
    }
}