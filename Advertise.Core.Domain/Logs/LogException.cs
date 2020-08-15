using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Logs
{
    public class LogException : BaseEntity
    {
        public virtual string Message { get; set; }
        public virtual string StackTrace { get; set; }
        public virtual string Method { get; set; }
    }
}