using System;
using FluentValidation;

namespace Advertise.Data.Validation.Common
{
    public static class FluentValidationExtention
    {
        public static IRuleBuilderOptions<TItem, Guid> IsNotNullGuid<TItem>(this IRuleBuilder<TItem, Guid> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new IsNotNullGuidValidate());
        }

        public static IRuleBuilderOptions<TItem, Guid?> IsNotNullGuid<TItem>(this IRuleBuilder<TItem, Guid?> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new IsNotNullGuidValidate());
        }
    }
}