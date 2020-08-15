using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Manufacturers
{
    public class ManufacturerCreateModel : BaseModel
    {
        public CountryType? Country { get; set; }
        public IEnumerable<SelectList> CountryList { get; set; }
        public string Name { get; set; }
    }
}