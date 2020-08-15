using Advertise.Core.Model.Companies;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Companies
{
    public class CompanyAttachmentCreateValidator : BaseValidator<CompanyAttachmentCreateModel>
    {
        public CompanyAttachmentCreateValidator()
        {
            RuleFor(model => model.Title).NotNull().WithMessage("عنوان فایل را وارد کنید");
            RuleFor(model => model.Title).MinimumLength(2).MaximumLength(30).WithMessage("عنوان فایل باید بیشتر از 2 و کمتر از 30 کاراکتر باشد");
        }
    }
}