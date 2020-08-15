using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Settings
{
    public class Setting : BaseEntity
    {
        public virtual bool IsAllowViewingProfiles { get; set; }
        public virtual bool IsDefaultAvatarEnabled { get; set; }
        public virtual string FacebookPageUrl { get; set; }
        public virtual string GooglePlusPageUrl { get; set; }
        public virtual string InstagramPageUrl { get; set; }
        public virtual string LinkedinPageUrl { get; set; }
        public virtual string SiteDescription { get; set; }
        public virtual string SiteEmail { get; set; }
        public virtual string SiteShortTitle { get; set; }
        public virtual string SiteTitle { get; set; }
        public virtual string SiteVersion { get; set; }
        public virtual string TelegramPageUrl { get; set; }
        public virtual string VideoMaximumSizeBytes { get; set; }
    }
}