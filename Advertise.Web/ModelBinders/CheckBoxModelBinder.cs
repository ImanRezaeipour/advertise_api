using System.Web.Mvc;

namespace Advertise.Web.ModelBinders
{
    public class CheckBoxModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var modelState = new ModelState
            {
                Value = valueResult
            };
            if (valueResult.AttemptedValue == null)
                return null;

            var actualValue = new bool();
            try
            {
                switch (valueResult.AttemptedValue.ToLower())
                {
                    case "on":
                    case "true":
                    case "1":
                        actualValue = true;
                        break;
                    case "off":
                    case "false":
                    case "0":
                        actualValue = false;
                        break;
                }
            }
            catch
            {
                modelState.Errors.Add("فرمت اشتباه است");
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }
}
