using System;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Announces
{
   public class AnnounceOptionSearchModel : BaseSearchModel
    {
       public AnnounceType? Type { get; set; }
       public DateTime? Day { get; set; }
       public AnnouncePositionType? Position { get; set; }
       public Guid? CreatedById { get; set; }
    }
}
