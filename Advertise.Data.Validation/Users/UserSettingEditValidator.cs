using Advertise.Core.Model.Users;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Users
{
    public class UserSettingEditValidator:BaseValidator<UserSettingEditModel>
    {
        public UserSettingEditValidator()
        {
            RuleFor(model => model.Language).NotNull().WithMessage("زبان وارد شود");
            RuleFor(model => model.Theme).NotNull().WithMessage("تم وارد شود");
        }
    }
}
