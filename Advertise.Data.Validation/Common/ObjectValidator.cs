using FluentValidation;
using FluentValidation.Results;

namespace Advertise.Data.Validation.Common
{
    public class ObjectValidator : BaseValidator<object> 
    {
        //public override ValidationResult Validate(ValidationContext<object> context)
        //{
        //    return context.InstanceToValidate == null 
        //        ? new ValidationResult(new [] { new ValidationFailure("Object", "Object cannot be null") }) 
        //        : base.Validate(context.InstanceToValidate);
        //}

        public ObjectValidator()
        {
            RuleFor(model => model).NotNull().WithMessage("Object cannot be null.");
        }
    }
}