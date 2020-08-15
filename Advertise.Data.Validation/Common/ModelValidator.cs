using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace Advertise.Data.Validation.Common
{
    public class ModelValidator : IModelValidator
    {
        public void Validate<TValidator, TModel>(TModel model) where TValidator : AbstractValidator<TModel>, new()
        {
            var validator = new TValidator();
            validator.ValidateAndThrow(model);
        }
    }
}