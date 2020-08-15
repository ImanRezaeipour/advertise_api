using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Keywords
{
    public class KeywordSearchModel : BaseSearchModel
    {
        public DateTime? CreatedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}