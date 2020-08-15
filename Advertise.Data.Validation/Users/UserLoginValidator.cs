using Advertise.Core.Model.Users;
using Advertise.Data.Validation.Common;

namespace Advertise.Data.Validation.Users
 {
     public class UserLoginValidator : BaseValidator<UserLoginModel>
     {
         public UserLoginValidator()
         {
             //RuleFor(model => model.UserName).MustAsync(userService.IsExistByEmailCancellationTokenAsync).WithMessage("این پست الکترونیک ثبت نشده است");
             //RuleFor(model => model.Password).MustAsync((model, password, token) => userService.IsExistByEmailAndPasswordAsync(model.UserName, password, token)).WithMessage("کلمه عبور اشتباه است");
             //RuleFor(model => model.UserName).MustAsync(userService.IsBanByEmailAsync).WithMessage("حساب کاربری شما مسدود شده است");
             //RuleFor(model => model.UserName).MustAsync((username, token) => userService.IsEmailConfirmedByEmailAsync(username, http, token)).WithMessage("می بایست ابتدا حساب کاربری خود را تایید نمایید");
             //RuleFor(model => model.UserName).MustAsync(userService.IsLockedOutAsync).WithMessage("حساب کاربری شما قفل می باشد");
         }
     }
 }