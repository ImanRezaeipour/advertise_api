namespace Advertise.Core.Domain.Common
{
    public class BaseReview : BaseEntity
    {
        public virtual string Body { get; set; }
        public virtual string Title { get; set; }
        public virtual bool? IsActive { get; set; }
    }
}
