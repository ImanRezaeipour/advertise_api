using Advertise.Core.Model.Users;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Users
{
    public class UserEditValidator : BaseValidator<UserEditModel>
    {
        public UserEditValidator()
        {
            RuleFor(model => model.HomeNumber).MinimumLength(11).WithMessage("شماره تلفن باید 11رقم باشد")
                .MaximumLength(11).WithMessage("شماره تلفن باید 11رقم باشد");
            RuleFor(model => model.HomeNumber).Matches("^[۰-۹0-9_]*$").WithMessage("شماره تلفن باید عدد باشد");
            RuleFor(model => model.PhoneNumber).MinimumLength(11).WithMessage("شماره موبایل باید 11رقم باشد")
                .MaximumLength(11).WithMessage("شماره موبایل باید 11رقم باشد");
            RuleFor(model => model.PhoneNumber).Matches("^[۰-۹0-9_]*$").WithMessage("شماره موبایل باید عدد باشد");
            RuleFor(model => model.NationalCode).MinimumLength(10).WithMessage("کد ملی باید 10 رقم باشد")
                .MaximumLength(10).WithMessage("کد ملی باید 10 رقم باشد");
            RuleFor(model => model.NationalCode).Matches("^[۰-۹0-9_]*$").WithMessage("کدملی باید عدد وارد شود");
        }
    }
}