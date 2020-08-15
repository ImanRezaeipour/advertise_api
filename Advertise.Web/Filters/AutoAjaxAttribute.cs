using System;
using System.Web.Mvc;

namespace Advertise.Web.Filters
{
    public class AutoAjaxAttribute :ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.ActionName;
            var isAjax = actionName.EndsWith("Ajax");
            if (isAjax)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    throw new InvalidOperationException("درخواست باید به صورت ایجکس ارسال شود");
                }
            }
           
        }
    }
}
