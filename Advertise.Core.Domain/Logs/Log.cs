using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Logs
{
    public class Log : BaseEntity
    {
        public virtual string Message { get; set; }
        public virtual string Method { get; set; }
        public virtual string Input { get; set; }
    }
}