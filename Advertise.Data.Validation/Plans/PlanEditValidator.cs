using Advertise.Core.Model.Plans;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Plans
{
    public class PlanEditValidator : BaseValidator<PlanEditModel>
    {
        public PlanEditValidator()
        {
            RuleFor(model => model.Title).NotNull().WithMessage("عنوان را وارد کنید");
            RuleFor(model => model.Title).MinimumLength(2).MaximumLength(30).WithMessage("عنوان باید بیشتر از 2 و کمتر از 30 کاراکتر باشد");
            RuleFor(model => model.Code).NotNull().WithMessage("کد پلن را وارد کنید");
            RuleFor(model => model.Color).NotNull().WithMessage("رنگ را وارد کنید");
            RuleFor(model => model.PlanDuration).NotNull().WithMessage("مدت زمان را وارد کنید");
            RuleFor(model => model.PreviousePrice).NotNull().WithMessage("مبلغ قبلی را وارد کنید");
            RuleFor(model => model.Price).NotNull().WithMessage("مبلغ را وارد کنید");
            RuleFor(model => model.RoleId).NotNull().WithMessage("نقش را وارد کنید");
        }
    }
}
