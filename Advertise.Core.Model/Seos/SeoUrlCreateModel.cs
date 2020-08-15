using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Seos
{
    public class SeoUrlCreateModel : BaseModel
    {
        public string AbsoulateUrl { get; set; }
        public string CurrentUrl { get; set; }
        public bool IsActive { get; set; }
        public RedirectionType Redirection { get; set; }
        public IEnumerable<SelectList> RedirectionTypeList { get; set; }
    }
}