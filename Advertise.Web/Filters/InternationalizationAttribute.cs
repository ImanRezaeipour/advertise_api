using System;
using System.Threading;
using System.Web.Mvc;
using Advertise.Service.Localizations;

namespace Advertise.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class InternationalizationAttribute : ActionFilterAttribute
    {
        #region Public Methods

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string cultureName = null;

            var lang = (string)filterContext.RouteData.Values["lang"];
            if (lang != null && string.IsNullOrEmpty(lang) == false)
            {
                switch (lang)
                {
                    case "fa":
                        cultureName = "fa-IR";
                        break;

                    case "en":
                        cultureName = "en-US";
                        break;

                    case "ar":
                        cultureName = "ar-SA";
                        break;

                    default:
                        filterContext.Result = new RedirectResult("/notfound");
                        break;
                        //var cultureCookie = filterContext.HttpContext.Request.Cookies["_culture"];
                        //if (cultureCookie != null)
                        //{
                        //    cultureName = cultureCookie.Value;
                        //}
                        //else if (filterContext.HttpContext.Novinak.Identity.IsAuthenticated)
                        //{
                        //    //cultureName = await _userSettingService.GetMyLanguageAsync();
                        //}
                        //else
                        //{
                        //    cultureName = "fa-IR";
                        //}
                        //break;
                }
            }
            else if (filterContext.HttpContext.Request.Cookies["_culture"] != null)
            {
                cultureName = filterContext.HttpContext.Request.Cookies["_culture"].Value;
            }
            else  // SUBDOMAIN
            {
                //cultureName = "fa-IR";
                filterContext.Result = new RedirectResult("/");
            }

            cultureName = CultureHelper.GetImplementedCulture(cultureName);

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            base.OnActionExecuting(filterContext);
        }

        #endregion Public Methods
    }
}