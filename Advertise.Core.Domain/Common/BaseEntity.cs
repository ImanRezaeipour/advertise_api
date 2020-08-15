using System;
using System.ComponentModel.DataAnnotations;

namespace Advertise.Core.Domain.Common
{
    public abstract class BaseEntity : Entity
    {
        public virtual DateTime? CreatedOn { get; set; }
        public virtual bool? IsDelete { get; set; }
        [Timestamp]
        public virtual byte?[] RowVersion { get; set; }
    }
}