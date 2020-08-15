using Advertise.Core.Model.Complaints;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Complaints
{
    public class ComplaintCreateValidator : BaseValidator<ComplaintCreateModel>
    {
        public ComplaintCreateValidator()
        {
            RuleFor(model => model.Body).NotNull().WithMessage("متن شکایت را وارد کنید");
            RuleFor(model => model.Body).MinimumLength(10).MaximumLength(200).WithMessage("متن شکایت باید بیشتر از 10 و کمتر از 200 کاراکتر باشد");
            RuleFor(model => model.Title).NotNull().WithMessage("عنوان را وارد کنید");
            RuleFor(model => model.Title).MinimumLength(2).MaximumLength(30).WithMessage("عنوان باید بیشتر از 2 و کمتر از 30 کاراکتر باشد");
        }
    }
}
