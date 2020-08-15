using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Announces
{
    public class AnnounceOptionCreateModel : BaseModel
    {
        public string Title { get; set; }
        public int? Order { get; set; }
        public decimal? Price { get; set; }
        public AnnouncePositionType? PositionType { get; set; }
        public IEnumerable<SelectList> PositionList { get; set; }
        public AnnounceType? Type { get; set; }
        public IEnumerable<SelectList> TypeList { get; set; }
    }
}