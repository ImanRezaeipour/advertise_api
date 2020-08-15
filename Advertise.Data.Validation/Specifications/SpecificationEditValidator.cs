using Advertise.Core.Model.Specifications;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Specifications
{
    public class SpecificationEditValidator : BaseValidator<SpecificationEditModel>
    {
        public SpecificationEditValidator()
        {
            RuleFor(model => model.CategoryId).NotNull().WithMessage("لطفا یک دسته را انتخاب نمایید");
            RuleFor(model => model.Description).NotNull().WithMessage("توضیحات را اوارد کنید");
            RuleFor(model => model.Description).MinimumLength(2).MaximumLength(250).WithMessage("توضیحات باید بیشتر از 2 و کمتر از250 کاراکتر باشد");
            RuleFor(model => model.Title).NotNull().WithMessage("نام را اوارد کنید");
            RuleFor(model => model.Title).MinimumLength(2).MaximumLength(70).WithMessage("نام باید بیشتر از 2 و کمتر از70 کاراکتر باشد");
            RuleFor(model => model.Order).NotNull().WithMessage("الویت را وارد نمایید");
            RuleFor(model => model.Type).NotNull().WithMessage("لطفا یک نوع را انتخاب نمایید");
        }
    }
}