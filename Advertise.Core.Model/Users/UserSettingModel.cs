using Advertise.Core.Types;

namespace Advertise.Core.Model.Users
{
    public class UserSettingModel
    {
        public bool IsEnableDateOfBirth { get; set; }
        public bool IsEnableSpecificationletter { get; set; }
        public bool IsHideSpecificationletterBlock { get; set; }
        public LanguageType Language { get; set; }
        public ThemeType Theme { get; set; }
        public string UserLastName { get; set; }
    }
}