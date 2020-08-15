using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Catalogs
{
    public class CatalogListModel : BaseModel
    {
        public IEnumerable<CatalogModel> Catalogs { get; set; }
        public IEnumerable<SelectList> PageSizeList { get; set; }
        public CatalogSearchModel SearchRequest { get; set; }
        public IEnumerable<SelectList> SortDirectionList { get; set; }
        public IEnumerable<SelectList> SortMemberList { get; set; }
    }
}