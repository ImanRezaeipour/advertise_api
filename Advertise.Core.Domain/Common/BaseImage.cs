namespace Advertise.Core.Domain.Common
{
    public class BaseImage : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string FileName { get; set; }
        public virtual string FileSize { get; set; }
        public virtual string FileExtension { get; set; }
        public virtual string FileDimension { get; set; }
    }
}
