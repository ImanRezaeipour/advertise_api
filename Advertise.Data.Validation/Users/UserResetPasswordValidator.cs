using Advertise.Core.Model.Users;
using Advertise.Data.Validation.Common;

namespace Advertise.Data.Validation.Users
{
    public class UserResetPasswordValidator:BaseValidator<UserResetPasswordModel>
    {
        public UserResetPasswordValidator()
        {
            //RuleFor(model => model.Email).MustAsync(userService.IsExistByEmailCancellationTokenAsync).WithMessage("این نام کاربری وجود ندارد");
        }
    }
}
