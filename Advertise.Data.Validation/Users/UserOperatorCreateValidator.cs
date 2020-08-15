using Advertise.Core.Model.Users;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Users
{
   public class UserOperatorCreateValidator:BaseValidator<UserOperatorCreateModel>
    {
        public UserOperatorCreateValidator()
        {
            RuleFor(model => model.Alias).NotNull().WithMessage("نام دامین را وارد کنید");
            RuleFor(model => model.CompanyTitle).NotNull().WithMessage("نام شرکت را وارد کنید");
            RuleFor(model => model.Alias).Matches(@"^([A-Za-z]{1}[A-Za-z0-9\-]{1,}[A-Za-z0-9]{1,})$")
                .WithMessage("شما می‌بایست تنها از حروف الفبای انگلیسی، خط‌فاصله و اعداد در نام دامنه استفاده نمایید که این نام الزاما با حروف الفبای انگلیسی آغاز می‌شود و حرف آخر نمی‌تواند خط‌فاصله باشد");
            RuleFor(model => model.CategoryId).NotNull().WithMessage("یک دسته را انتخاب کنید");
            RuleFor(model => model.Email).NotNull().WithMessage("ایمیل را وارد کنید");
            RuleFor(model => model.Password).NotNull().WithMessage("پسورد وارد شود");
            RuleFor(model => model.Password).MinimumLength(6).MaximumLength(100).WithMessage("پسورد باید بیشتر از 6 کاراکتر و کمتراز 100 کاراکتر باشد");
            RuleFor(model => model.PaymentType).NotNull().WithMessage("نوع پرداخت را انتخاب کنید");
            RuleFor(model => model.RoleId).NotNull().WithMessage("یک نقش را انتخاب کنید");
            RuleFor(model => model.MobileNumber).NotNull().WithMessage("موبایل وارد شود");
            RuleFor(model => model.MobileNumber).MinimumLength(11).MaximumLength(11).WithMessage("موبایل باید 11 رقم باشد");
            RuleFor(model => model.MobileNumber).Matches("^[۰-۹0-9_]*$").WithMessage("موبایل عدد وارد شود");
            //RuleFor(model => model.Email).MustAsync(async (email,token) => !await userService.IsExistByEmailCancellationTokenAsync(email,token)).WithMessage("این پست الکترونیک وجود دارد");
        }
    }
}
