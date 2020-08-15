using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Manufacturers
{
    public class ManufacturerListModel : BaseModel
    {
        public IEnumerable<ManufacturerModel> Manufacturers {get;set;}
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public ManufacturerSearchModel SearchModel { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}