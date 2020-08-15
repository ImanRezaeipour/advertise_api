using Advertise.Core.Model.Plans;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Plans
{
    public class PlanDiscountEditValidator : BaseValidator<PlanDiscountEditModel>
    {
        public PlanDiscountEditValidator()
        {
            RuleFor(model => model.Code).NotNull().WithMessage("کد تخفیف را وارد کنید");
            RuleFor(model => model.Code).MinimumLength(6).MaximumLength(10).WithMessage("کد تخفیف باید بیشتر از 6 و کتر از 10 کاراکتر باشد");
            RuleFor(model => model.Count).NotNull().WithMessage("تعداد تخفیف را وارد کنید");
            RuleFor(model => model.Max).NotNull().WithMessage("حداکثر مبلغ تخفیف را وارد کنید");
            RuleFor(model => model.Percent).NotNull().WithMessage("درصد تخفیف را وارد کنید");
        }
    }
}
