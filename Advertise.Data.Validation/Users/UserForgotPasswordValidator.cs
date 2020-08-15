using Advertise.Core.Model.Users;
using Advertise.Data.Validation.Common;

namespace Advertise.Data.Validation.Users
{
    public class UserForgotPasswordValidator : BaseValidator<UserForgotPasswordModel>
    {
        public UserForgotPasswordValidator()
        {
            //RuleFor(model => model.Email).MustAsync(userService.IsExistByEmailCancellationTokenAsync).WithMessage("این پست الکترونیک ثبت نشده است");
            //RuleFor(model => model.Email).MustAsync((username, token) => userService.IsEmailConfirmedByEmailAsync(username,http, token)).WithMessage("این پست الکترونیک تایید نشده است");
        }
    }
}
