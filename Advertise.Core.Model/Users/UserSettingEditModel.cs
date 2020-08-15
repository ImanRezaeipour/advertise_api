using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.Users
{
    public class UserSettingEditModel : BaseModel
    {
        public Guid Id { get; set; }
        public bool IsEnableDateOfBirth { get; set; }
        public bool IsEnableSpecificationletter { get; set; }
        public bool IsHideSpecificationletterBlock { get; set; }
        public LanguageType Language { get; set; }
        public IEnumerable<SelectList> LanguageList { get; set; }
        public ThemeType Theme { get; set; }
        public IEnumerable<SelectList> ThemeList { get; set; }
    }
}