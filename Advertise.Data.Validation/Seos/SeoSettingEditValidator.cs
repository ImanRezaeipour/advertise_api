using Advertise.Core.Model.Seos;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Seos
{
    public class SeoSettingEditValidator:BaseValidator<SeoSettingEditModel>
    {
        public SeoSettingEditValidator()
        {
            RuleFor(model => model.WwwRequirement).NotNull().WithMessage("وضعیت را وارد کنید www");
        }
    }
}
