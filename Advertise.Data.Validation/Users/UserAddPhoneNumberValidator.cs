using Advertise.Core.Model.Users;
using Advertise.Data.Validation.Common;

namespace Advertise.Data.Validation.Users
{
    public class UserAddPhoneNumberValidator:BaseValidator<UserAddPhoneNumberModel>
    {
        public UserAddPhoneNumberValidator()
        {
            //RuleFor(model => model.Number).NotNull().WithMessage("شماره موبایل را وارد کنید");
            //RuleFor(model => model.Number).MinimumLength(11).MaximumLength(11).WithMessage("شماره موبایل 11 رقم باشد");
            //RuleFor(model => model.Number).Matches("^[۰-۹0-9_]*$").WithMessage("شماره موبایل عدد باشد");
        }
    }
}
