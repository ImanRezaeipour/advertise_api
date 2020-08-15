using System;
using System.Web;
using Advertise.Core.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Advertise.Core.Managers.WebContext
{
    public class WebContextManager : IWebContextManager
    {
        public HttpContext CurrentContext => HttpContext.Current;

        public string CurrentRequestBrowser => HttpContext.Current.Request.GetBrowser();

        public string CurrentRequestIp => HttpContext.Current.Request.GetIp();

        public Uri CurrentRequestUrl => HttpContext.Current.Request.Url;

        public IAuthenticationManager CurrentUserAuthenticate => HttpContext.Current.GetOwinContext().Authentication;

        public Guid CurrentUserId => HttpContext.Current.User.Identity.IsAuthenticated ? Guid.Parse(HttpContext.Current.User.Identity.GetUserId()) : Guid.Empty;
    }
}