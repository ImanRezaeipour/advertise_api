using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Settings
{
    public class SettingModel : BaseModel
    {
        public string FacebookPageUrl { get; set; }
        public string GooglePlusPageUrl { get; set; }
        public string InstagramPageUrl { get; set; }
        public bool IsAllowViewingProfiles { get; set; }
        public bool IsDefaultAvatarEnabled { get; set; }
        public string LinkedinPageUrl { get; set; }
        public string SiteDescription { get; set; }
        public string SiteEmail { get; set; }
        public string SiteShortTitle { get; set; }
        public string SiteTitle { get; set; }
        public string SiteVersion { get; set; }
        public string TelegramPageUrl { get; set; }
        public string VideoMaximumSizeBytes { get; set; }
    }
}