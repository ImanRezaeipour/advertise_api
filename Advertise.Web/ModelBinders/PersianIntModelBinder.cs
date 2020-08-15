using System;
using System.Globalization;
using System.Web.Mvc;
using Advertise.Core.Extensions;

namespace Advertise.Web.ModelBinders
{
    public class PersianIntModelBinder : IModelBinder
    {
        #region Public Methods

        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
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
                actualValue = int.Parse(value,
                    CultureInfo.InvariantCulture);
            }
            catch (FormatException e)
            {
                modelState.Errors.Add("فقط از اعداد استفاده کنید");
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }

        #endregion Public Methods
    }
}