using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Seos
{
    public class SeoSettingModel : BaseModel
    {
        public string CustomHeadTags { get; set; }
        public string DefaultMetaDescription { get; set; }
        public string DefaultMetaKeywords { get; set; }
        public string DefaultTitle { get; set; }
        public string GenerateMetaDescription { get; set; }
        public bool HasConvertNonWesternChars { get; set; }
        public bool HasOpenGraphMetaTags { get; set; }
        public bool HasTwitterMetaTags { get; set; }
        public bool IsAllowUnicodeChars { get; set; }
        public bool IsCanonicalUrlEnabled { get; set; }
        public bool IsCssBundlingEnabled { get; set; }
        public bool IsJsBundlingEnabled { get; set; }
        public string PageTitleSeparator { get; set; }
        public WwwRequirementType WwwRequirement { get; set; }
    }
}