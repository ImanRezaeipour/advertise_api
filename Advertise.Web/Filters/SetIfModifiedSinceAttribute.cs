using System;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace Advertise.Web.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class SetIfModifiedSinceAttribute : ActionFilterAttribute
    {
        public string Parameter { set; get; }
        public string BasePath { set; get; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = filterContext.RequestContext.HttpContext.Response;
            var request = filterContext.RequestContext.HttpContext.Request;

            var path = GetPath(filterContext);
            if (string.IsNullOrWhiteSpace(path))
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                filterContext.Result = new EmptyResult();
                return;
            }

            var lastWriteTime = File.GetLastWriteTime(path);
            response.Cache.SetLastModified(lastWriteTime.ToUniversalTime());

            var header = request.Headers["If-Modified-Since"];
            if (string.IsNullOrWhiteSpace(header)) return;
            DateTime isModifiedSince;
            if (!DateTime.TryParse(header, out isModifiedSince) || isModifiedSince <= lastWriteTime) return;
            response.StatusCode = (int)HttpStatusCode.NotModified;
            response.SuppressContent = true;
            filterContext.Result = new EmptyResult();
        }

        string GetPath(ActionExecutingContext filterContext)
        {
            if (!filterContext.ActionParameters.ContainsKey(Parameter)) return string.Empty;
            var name = filterContext.ActionParameters[Parameter] as string;
            if (string.IsNullOrWhiteSpace(name)) return string.Empty;

            var path = Path.GetFileName(name);
            path = filterContext.HttpContext.Server.MapPath($"{BasePath}/{path}");
            return !File.Exists(path) ? string.Empty : path;
        }
    }
}