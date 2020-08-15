using System.Web.Mvc;

namespace Advertise.Web.Filters
{
    public class RemoveServerHeaderFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = filterContext.HttpContext.Response;
            // for prevent attack
            response.Headers.Remove("server");
            response.Headers.Remove("x-powered-by");
            response.Headers.Remove("x-aspnet-version");
            response.Headers.Remove("x-sourcefiles");
            base.OnActionExecuting(filterContext);
        }
    }
}