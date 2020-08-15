using System;
using FluentValidation.Validators;

namespace Advertise.Data.Validation.Common
{
    public class IsNotNullGuidValidate : PropertyValidator
    {
        public IsNotNullGuidValidate() : base("فیلد {PropertyName} را انتخاب کنید")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null)
                return false;
          
            Guid value;
            if (Guid.TryParse(context.PropertyValue.ToString(), out value) == false)
                return false;

            if (value == Guid.Empty)
                return false;

            return true;
        }
    }
}