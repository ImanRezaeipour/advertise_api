using Advertise.Core.Model.Users;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Users
{
    public class UserRegisterValidator : BaseValidator<UserRegisterModel>
    {
        public UserRegisterValidator()
        {
            RuleFor(model => model.Email).NotNull().WithMessage("ایمیل را وارد کنید");
            //RuleFor(model => model.Password).NotNull().WithMessage("پسورد را وارد کنید");
            //RuleFor(model => model.Password).MinimumLength(6).MaximumLength(100).WithMessage("پسورد باید بیشتر از6 و کمتر از 100 کاراکتر باشد");
            //RuleFor(model => model.TermOfService).NotNull().WithMessage("قوانین را قبول ندارید؟");
            //RuleFor(model => model.Email).MustAsync(async (email, token) => !await userService.IsExistByEmailCancellationTokenAsync(email,token)).WithMessage("این پست الکترونیک وجود دارد");
        }
    }
}