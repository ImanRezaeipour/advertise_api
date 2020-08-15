using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Users;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Newsletters
{
   public class Newsletter :BaseEntity
    {
        public virtual string Email { get; set; }
        public virtual MeetType? Meet { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}
