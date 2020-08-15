using System.Collections.Generic;
using Advertise.Core.Domain.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Seos
{
    public class SeoSetting : BaseEntity
    {
        public virtual bool IsAllowUnicodeChars { get; set; }
        public virtual bool IsCanonicalUrlEnabled { get; set; }
        public virtual bool HasConvertNonWesternChars { get; set; }
        public virtual string CustomHeadTags { get; set; }
        public virtual string DefaultMetaDescription { get; set; }
        public virtual string DefaultMetaKeywords { get; set; }
        public virtual string DefaultTitle { get; set; }
        public virtual bool IsCssBundlingEnabled { get; set; }
        public virtual bool IsJsBundlingEnabled { get; set; }
        public virtual string GenerateMetaDescription { get; set; }
        public virtual bool HasOpenGraphMetaTags { get; set; }
        public virtual string PageTitleSeparator { get; set; }
        public List<string> ReservedUrlRecordSlugs { get; set; }
        public virtual bool HasTwitterMetaTags { get; set; }
        public virtual WwwRequirementType WwwRequirement { get; set; }
    }
}