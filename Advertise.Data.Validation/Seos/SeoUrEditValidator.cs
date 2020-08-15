using Advertise.Core.Model.Seos;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Seos
{
    public class SeoUrlEditValidator : BaseValidator<SeoUrlEditModel>
    {
        public SeoUrlEditValidator()
        {
            RuleFor(model => model.AbsoulateUrl).NotNull().WithMessage("آدرس قبلی را وارد کنید");
            RuleFor(model => model.CurrentUrl).NotNull().WithMessage("آدرس جدید را وارد کنید");
            RuleFor(model => model.Redirection).NotNull().WithMessage("نوع انتقال را وارد کنید");
        }
    }
}
