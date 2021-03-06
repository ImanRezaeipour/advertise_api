using System;
using System.Globalization;
using System.Web.Mvc;

namespace Advertise.Web.ModelBinders
{
    public class PersianDateModelBinder : IModelBinder
    {
        #region Public Methods

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var modelState = new ModelState
            {
                Value = valueResult
            };
            object actualValue = new DateTime(1989, 1, 15);
            try
            {
                var parts = valueResult.AttemptedValue.Split('/'); //ex. 1391/1/19
                if (parts.Length != 3) return actualValue;
                var year = int.Parse(parts[0]);
                var month = int.Parse(parts[1]);
                var day = int.Parse(parts[2]);
                actualValue = new DateTime(year, month, day, new PersianCalendar());
            }
            catch (FormatException e)
            {
                modelState.Errors.Add("تاریخ را به شکل صحیح [ به عنوان مثال 1371/9/28] وارد کنید");
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }

        #endregion Public Methods
    }
}