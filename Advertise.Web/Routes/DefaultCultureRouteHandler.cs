using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Advertise.Web.Routes
{
    public class DefaultCultureRouteHandler : MvcRouteHandler
    {
        #region Protected Methods

        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var route = (Route)requestContext.RouteData.Route;

            if (!route.Url.Contains("{lang}"))
                route.Url = "{lang}/" + route.Url;
            if (route.Defaults == null)
            {
                route.Defaults = new RouteValueDictionary
                {
                    {"lang", GetCultureLanguage()}
                };
            }
            else
            {
                route.Defaults["lang"] = GetCultureLanguage();
            }

            return base.GetHttpHandler(requestContext);
        }

        #endregion Protected Methods

        #region Private Methods


        private string GetCultureLanguage()
        {
            string cultureName = null;
            var cultureCookie = HttpContext.Current.Request.Cookies["_culture"];
            if (cultureCookie != null)
            {
                cultureName = cultureCookie.Value;
            }
            else if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                cultureName = "fa-IR";// await StructureMapObjectFactory.Container.GetInstance<IUserSettingService>().GetMyLanguageAsync();
            }
            else
            {
                cultureName = "fa-IR";
            }

            switch (cultureName)
            {
                case "fa-IR":
                    return "fa";

                case "en-US":
                    return "en";

                case "ar-SA":
                    return "ar";

                default:
                    return "fa";
            }
        }

        #endregion Private Methods
    }
}