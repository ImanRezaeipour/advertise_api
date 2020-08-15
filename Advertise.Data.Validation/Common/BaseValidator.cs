using FluentValidation;

namespace Advertise.Data.Validation.Common
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        protected BaseValidator()
        {
            //CascadeMode = CascadeMode.StopOnFirstFailure;
            
            //RuleFor(model => model).NotNull().WithMessage("Model is null.");
        }
    }
}