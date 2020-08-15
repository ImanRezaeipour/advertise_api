using System;
using Advertise.Core.Domain.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Domain.Users
{
    public class UserSetting : BaseEntity
    {
        public LanguageType? Language { get; set; }
        public ThemeType? Theme { get; set; }
        public bool? IsEnableSpecificationletter { get; set; }
        public bool? IsHideSpecificationletterBlock { get; set; }
        public bool? IsEnableDateOfBirth { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Guid? CreatedById { get; set; }
    }
}