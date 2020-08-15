using System;
using System.Web.Mvc;

namespace Advertise.Web.ModelBinders
{
    public class StringToDateModelBinder : IModelBinder
    {
        #region Public Methods

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var modelState = new ModelState
            {
                Value = valueResult
            };
            object actualValue = null;
            try
            {
                //todo: باید طبق این کامنت از تاریخ شمسی به تاریخ میلادی تغییر یابد که باید از کتابخانه مناسب استفاده شود
                //actualValue = PersianDateTime.Parse(valueResult.AttemptedValue).ToDateTime();
                actualValue = valueResult.AttemptedValue;
            }
            catch (FormatException e)
            {
                modelState.Errors.Add("تاریخ را به شکل صحیح [ به عنوان مثال 1399/02/01] وارد کنید");
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }

        #endregion Public Methods
    }
}