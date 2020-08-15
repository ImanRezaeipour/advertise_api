using System;
using System.Web;
using Microsoft.Owin.Security;

namespace Advertise.Core.Managers.WebContext
{
    public interface IWebContextManager
    {
        HttpContext CurrentContext { get; }
        string CurrentRequestBrowser { get; }
        string CurrentRequestIp { get; }
        Uri CurrentRequestUrl { get; }
        IAuthenticationManager CurrentUserAuthenticate { get; }
        Guid CurrentUserId { get; }
    }
}