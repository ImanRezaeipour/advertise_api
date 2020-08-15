using Advertise.Core.Domain.Common;

namespace Advertise.Core.Domain.Localizations
{
    public class LocalizationSetting : BaseEntity
    {
        public virtual int DefaultAdminLanguageId { get; set; }
        public virtual bool UseImagesForLanguageSelection { get; set; }
        public virtual bool SeoFriendlyUrlsForLanguagesEnabled { get; set; }
        public virtual bool AutomaticallyDetectLanguage { get; set; }
        public virtual bool LoadAllLocaleRecordsOnStartup { get; set; }
        public virtual bool LoadAllLocalizedPropertiesOnStartup { get; set; }
        public virtual bool LoadAllUrlRecordsOnStartup { get; set; }
        public virtual bool IgnoreRtlPropertyForAdminArea { get; set; }
    }
}