using Advertise.Core.Model.Users;
using Advertise.Data.Validation.Common;

namespace Advertise.Data.Validation.Users
{
    public class UserVerifyPhoneNumberValidator :BaseValidator<UserVerifyPhoneNumberModel>
    {
        public UserVerifyPhoneNumberValidator()
        {
            //RuleFor(model => model.UserId).MustAsync(userService.IsExistByIdCancellationTokenAsync).WithMessage("این نام کاربری وجود ندارد");
            //RuleFor(model => model.UserId).MustAsync(userService.IsBanByIdAsync).WithMessage("حساب کاربری شما مسدود شده است");
            //RuleFor(model => model.UserId).MustAsync(userService.IsLockedOutAsync).WithMessage("حساب کاربری شما قفل شده است");
        }
    }
}
