using Advertise.Core.Model.Tags;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Tags
{
   public class TagCreateValidator:BaseValidator<TagCreateModel>
    {
        public TagCreateValidator()
        {
            RuleFor(model => model.Title).NotNull().WithMessage("عنوان را وارد کنید");
            RuleFor(model => model.CostValue).Matches("^[۰-۹0-9_]*$").WithMessage("عدد وارد شود");
        }
    }
}
