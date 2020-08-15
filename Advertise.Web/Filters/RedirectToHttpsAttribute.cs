using System;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Advertise.Web.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class RedirectToHttpsAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly bool permanent;

        public RedirectToHttpsAttribute(bool permanent)
        {
            this.permanent = permanent;
        }

        public bool Permanent
        {
            get { return this.permanent; }
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (!filterContext.HttpContext.Request.IsSecureConnection)
            {
                this.HandleNonHttpsRequest(filterContext);
            }
        }

        protected virtual void HandleNonHttpsRequest(AuthorizationContext filterContext)
        {
            // Only redirect for GET requests, otherwise the browser might not propagate the verb and request body correctly.
            if (!string.Equals(
                filterContext.HttpContext.Request.HttpMethod,
                WebRequestMethods.Http.Get,
                StringComparison.OrdinalIgnoreCase))
            {
                // The RequireHttpsAttribute throws an InvalidOperationException. Some bots and spiders make HEAD
                // requests (to reduce bandwidth) and we don't want them to see a 500-Internal Server Error. A 405
                // Method Not Allowed would be more appropriate.
                throw new HttpException((int)HttpStatusCode.Forbidden, "Forbidden");
            }

            string url = "https://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
            filterContext.Result = new RedirectResult(url, this.permanent);
        }
    }
}