using System;
using System.Web.Mvc;

namespace Advertise.Web.Filters
{
    public class CustomAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod.Contains("Post"))
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                throw new ArgumentNullException(nameof(filterContext));
            }
        }
    }
}
