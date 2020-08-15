using Advertise.Core.Model.Companies;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Companies
{
    public class CompanyImageEditValidator : BaseValidator<CompanyImageEditModel>
    {
        public CompanyImageEditValidator()
        {
            RuleFor(model => model.Title).NotNull().WithMessage("عنوان گالری عکس را وارد کنید");
            RuleFor(model => model.Title).MinimumLength(2).MaximumLength(30).WithMessage("عنوان فایل باید بیشتر از 2 و کمتر از 30 کاراکتر باشد");
        }
    }
}