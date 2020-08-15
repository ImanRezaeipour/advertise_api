using Advertise.Core.Model.Companies;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Companies
{
    public class CompanyEditValidator : BaseValidator<CompanyEditModel>
    {
        public CompanyEditValidator()
        {
            RuleFor(model => model.Title).NotNull().WithMessage("عنوان وارد شود");
            RuleFor(model => model.Title).MaximumLength(50).MinimumLength(2).WithMessage("عنوان باید بیشتر از2 و کمتر از 50 کاراکتر باشد");
            RuleFor(model => model.Location.PostalCode).MaximumLength(10).MinimumLength(10).WithMessage("کد پستی باید 10رقم باشد");
            RuleFor(model => model.Location.PostalCode).Matches("^[۰-۹0-9_]*$").WithMessage("کد پستی موبایل عدد وارد شود");
            RuleFor(model => model.Clearing).NotNull().WithMessage("روش تسویه حساب را انتخاب کنید");
            RuleFor(model => model.CategoryId).NotNull().WithMessage("دسته کاری را وارد شود");
            RuleFor(model => model.MobileNumber).Matches("^[۰-۹0-9_]*$").WithMessage("شماره موبایل عدد وارد شود");
            RuleFor(model => model.MobileNumber).MaximumLength(11).MinimumLength(11).WithMessage("شماره موبایل 11 رقم وارد شود");
            RuleFor(model => model.PhoneNumber).Matches("^[۰-۹0-9_]*$").WithMessage("شماره تلفن عدد وارد شود");
            RuleFor(model => model.PhoneNumber).MaximumLength(11).MinimumLength(11).WithMessage("شماره تلفن 11 رقم وارد شود");
            RuleFor(model => model.WebSite).Matches("^(https?://)?(www\\.)?([-a-z0-9]{1,63}\\.)*?[a-z0-9][-a-z0-9]{0,61}[a-z0-9]\\.[a-z]{2,6}(/[-\\w@\\+\\.~#\\?&/=%]*)?$").WithMessage("لطفا آدرس وب‌سایت معتبر وارد نمایید");
            RuleFor(model => model.Alias).NotNull().WithMessage("نام دامنه را وارد شود");
            RuleFor(model => model.Alias).Matches("^([A-Za-z]{1}[A-Za-z0-9-]{1,}[A-Za-z0-9]{1,})$").WithMessage("شما می‌بایست تنها از حروف الفبای انگلیسی، خط‌فاصله و اعداد در نام دامنه استفاده نمایید که این نام الزاما با حروف الفبای انگلیسی آغاز می‌شود و حرف آخر نمی‌تواند خط‌ فاصله باشد");
            RuleFor(model => model.Alias).MaximumLength(50).MinimumLength(6).WithMessage(" دامنه سایت باید بیشتر از 6 و کمتر از 50 کاراکتر باشد");
            RuleFor(model => model.Description).MaximumLength(1000000).WithMessage("توضیحات بیش از حد مجاز است");
            RuleFor(model => model.Email).EmailAddress().WithMessage("ایمیل شما معتبر نمی باشد");
            //RuleFor(model => model.Id).MustAsync(async (id , token) => await companyService.IsMineByIdAsync(id, httpContext,token)).WithMessage("عدم دسترسی برای ویرایش");
            //RuleFor(model => model.Alias).MustAsync(async (alias , token) => !await companyService.IsExistAliasCancellationTokenAsync(alias, httpContext,token)).WithMessage("این دامنه وجود دارد");
        }
    }
}