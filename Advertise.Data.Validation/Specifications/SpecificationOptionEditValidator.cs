using Advertise.Core.Model.Specifications;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Specifications
{
    public class SpecificationOptionEditValidator : BaseValidator<SpecificationOptionEditModel>
    {
        public SpecificationOptionEditValidator()
        {
            RuleFor(model => model.CategoryId).IsNotNullGuid().WithMessage("دسته را انتخاب کنید");
            RuleFor(model => model.SpecificationId).IsNotNullGuid().WithMessage("مشخصه را وارد کنید");
        }
    }
}