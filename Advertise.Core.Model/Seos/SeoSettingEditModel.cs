using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Seos
{
    public class SeoSettingEditModel : BaseModel
    {
        public string DefaultMetaDescription { get; set; }
        public string DefaultMetaKeywords { get; set; }
        public string DefaultTitle { get; set; }
        public bool HasOpenGraphMetaTags { get; set; }
        public bool HasTwitterMetaTags { get; set; }
        public Guid Id { get; set; }
        public bool IsCanonicalUrlEnabled { get; set; }
        public bool IsCssBundlingEnabled { get; set; }
        public bool IsJsBundlingEnabled { get; set; }
        public string PageTitleSeparator { get; set; }
        public WwwRequirementType WwwRequirement { get; set; }
        public IEnumerable<SelectList> WwwRequirementList { get; set; }
    }
}