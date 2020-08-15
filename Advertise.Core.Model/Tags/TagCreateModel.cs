using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Tags
{
    public class TagCreateModel : BaseModel
    {
        public ColorType Color { get; set; }
        public IEnumerable<SelectList> ColorTypeList { get; set; }
        public string CostValue { get; set; }
        public string Description { get; set; }
        public string DurationDay { get; set; }
        public bool IsActive { get; set; }
        public string LogoFileName { get; set; }
        public string Title { get; set; }
    }
}