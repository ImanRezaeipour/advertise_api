using System.Threading;
using System.Threading.Tasks;
using Advertise.Core.Model.Users;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Users
{
    public class UserEditMeValidator : BaseValidator<UserEditMeModel>
    {
        public UserEditMeValidator()
        {
            RuleFor(model => model.HomeNumber).MinimumLength(11)
                .MaximumLength(11).WithMessage("شماره تلفن باید 11رقم باشد");
            RuleFor(model => model.HomeNumber).Matches("^[۰-۹0-9_]*$").WithMessage("شماره تلفن باید عدد باشد");
            RuleFor(model => model.PhoneNumber).MinimumLength(11).WithMessage("شماره موبایل باید 11رقم باشد")
                .MaximumLength(11).WithMessage("شماره موبایل باید 11رقم باشد");
            RuleFor(model => model.PhoneNumber).Matches("^[۰-۹0-9_]*$").WithMessage("شماره موبایل باید عدد باشد");
            RuleFor(model => model.NationalCode).MinimumLength(10).WithMessage("کد ملی باید 10 رقم باشد")
                .MaximumLength(10).WithMessage("کد ملی باید 10 رقم باشد");
            RuleFor(model => model.NationalCode).Matches("^[۰-۹0-9_]*$").WithMessage("کدملی باید عدد وارد شود");
            RuleFor(model => model.UserName).Matches(@"^([A-Za-z]{1}[A-Za-z0-9\-]{1,}[A-Za-z0-9]{1,})$").WithMessage("شما می‌بایست تنها از حروف الفبای انگلیسی، خط‌فاصله و اعداد در نام دامنه استفاده نمایید که این نام الزاما با حروف الفبای انگلیسی آغاز می‌شود و حرف آخر نمی‌تواند خط‌فاصله باشد");
            RuleFor(model => model.UserName).NotNull().WithMessage("نام کاربری را وارد کنید");
            RuleFor(model => model.UserName).MinimumLength(6).WithMessage("نام کاربری باید بیشتر از 6 و کمتر از 50 کارکتر باشد").MaximumLength(50).WithMessage("نام کاربری باید بیشتر از 6 و کمتر از 50 کارکتر باشد");
            RuleFor(model => model.UserName).MustAsync(IsExistNovinakInUserNameAsync).WithMessage("وجود کلمه نویناک در نام کاربری");
            //RuleFor(model => model.UserName).MustAsync(userService.IsExistByUserNameCancellationTokenAsync).WithMessage("این نام کاربری وجود دارد");
        }
        
        private async Task<bool> IsExistNovinakInUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            return !userName.StartsWith("Novinak");
        }
    }
}