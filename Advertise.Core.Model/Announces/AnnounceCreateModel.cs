using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Announces
{
    public class AnnounceCreateModel : BaseModel
    {
        public Guid? AdsOptionId { get; set; }
        public IEnumerable<SelectList> AdsOptionPositionList { get; set; }
        public IEnumerable<AnnounceOptionModel> AnnounceOptions { get; set; }
        public IEnumerable<SelectList> AdsOptionTypeList { get; set; }
        public string CategeoryListJson { get; set; }
        public Guid? CategoryId { get; set; }
        public IEnumerable<SelectList> DurationList { get; set; }
        public DurationType? DurationType { get; set; }
        public Guid? EntityId { get; set; }
        public IEnumerable<SelectList> EntityList { get; set; }
        public string EntityName { get; set; }
        public decimal? FinalPrice { get; set; }
        public string ImageFileName { get; set; }
        public string Title { get; set; }
    }
}