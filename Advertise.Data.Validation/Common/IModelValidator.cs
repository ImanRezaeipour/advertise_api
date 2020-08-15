using FluentValidation;

namespace Advertise.Data.Validation.Common
{
    public interface IModelValidator
    {
        void Validate<TValidator, TModel>(TModel model) where TValidator : AbstractValidator<TModel>, new();
    }
}