using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Users
{
    public class UserOperator :BaseEntity
    {
        public virtual PaymentType? PaymentType { get; set; }
        public virtual decimal? Amount { get; set; }
        public virtual string Description { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}
