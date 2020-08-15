using Advertise.Core.Model.Companies;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Companies
{
    public class CompanyReviewEditValidator : BaseValidator<CompanyReviewEditModel>
    {
        public CompanyReviewEditValidator()
        {
            RuleFor(model => model.Body).NotNull().WithMessage("نقد و بررسی را وارد کنید");
            RuleFor(model => model.Title).NotNull().WithMessage("عنوان را وارد کنید");
            RuleFor(model => model.Title).MinimumLength(2).MaximumLength(30).WithMessage("عنوان باید بیشتر از 2 و کمتر از 30 کاراکتر باشد ");
            RuleFor(model => model.CompanyId).NotNull().WithMessage("دسته نقد را وارد کنید");
        }
    }
}