using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Cache;

namespace Advertise.Web.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DosFilterAttribute : ActionFilterAttribute
    {
        public HttpContextBase ContextBase { get; set; }

        public string Name { get; set; }

        public int Seconds { get; set; }

        public string Message { get; set; }
        
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var key = string.Concat(Name, "-", actionContext.HttpContext.Request.GetIp());
            var allowExecute = false;

            if (ContextBase.CacheRead(key) == null)
            {
                ContextBase.CacheInsertWithSeconds(key, true, Seconds);
                allowExecute = true;
            }

            if (allowExecute) return;

            if (!Message.HasValue())
                Message = "You may only perform this action every {n} seconds.";

            actionContext.Result = new ContentResult { Content = Message.Replace("{n}", Seconds.ToString(CultureInfo.InvariantCulture)) };
            // see 429 - Rate Limit Exceeded HTTP error
            actionContext.HttpContext.Response.StatusCode = 429;
        }

    }
}
