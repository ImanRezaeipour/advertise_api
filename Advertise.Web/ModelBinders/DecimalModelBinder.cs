using System;
using System.Globalization;
using System.Web.Mvc;
using Advertise.Core.Extensions;

namespace Advertise.Web.ModelBinders
{
    public class DecimalModelBinder : IModelBinder
    {
        #region Public Methods

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var modelState = new ModelState
            {
                Value = valueResult
            };
            if (valueResult.AttemptedValue == null) return null;
            object actualValue = null;
            try
            {
                var value = valueResult.AttemptedValue.GetEnglishNumber();
                actualValue = decimal.Parse(value,
                    CultureInfo.InvariantCulture);
            }
            catch (FormatException e)
            {
                modelState.Errors.Add("عدد مورد نظر به شکل صحیح (به عنوان مثال [۱۲/۱۱]) وارد کنید");
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }

        #endregion Public Methods
    }
}