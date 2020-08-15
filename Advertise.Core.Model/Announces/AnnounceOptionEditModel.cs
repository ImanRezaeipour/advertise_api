using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Announces
{
    public class AnnounceOptionEditModel : BaseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int? Order { get; set; }
        public decimal? Price { get; set; }
        public AnnouncePositionType? PositionType { get; set; }
        public IEnumerable<SelectList> PositionList { get; set; }
        public AnnounceType? Type { get; set; }
        public IEnumerable<SelectList> TypeList { get; set; }
    }
}