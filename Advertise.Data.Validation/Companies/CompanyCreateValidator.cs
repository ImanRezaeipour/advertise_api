using Advertise.Core.Model.Companies;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Companies
{
    public class CompanyCreateValidator:BaseValidator<CompanyCreateModel>
    {
        public CompanyCreateValidator()
        {
            RuleFor(model => model.Title).NotNull().WithMessage("عنوان وارد شود");
            RuleFor(model => model.CategoryId).NotNull().WithMessage("دسته کاری را وارد شود");
            RuleFor(model => model.MobileNumber).Matches("^[۰-۹0-9_]*$").WithMessage("شماره موبایل عدد وارد شود");
            RuleFor(model => model.MobileNumber).MaximumLength(11).MinimumLength(11).WithMessage("شماره موبایل 11 رقم وارد شود");
            RuleFor(model => model.Title).MaximumLength(30).MinimumLength(2).WithMessage("عنوان باید بیشتر از2 و کمتر از 30 کاراکتر باشد");
        }
    }
}