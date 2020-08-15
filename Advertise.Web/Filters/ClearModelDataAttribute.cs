using System;
using System.Web.Mvc;
using Advertise.Core.Constants;
using Advertise.Core.Exceptions;

namespace Advertise.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ClearModelDataAttribute : ActionFilterAttribute
    {
        #region Public Constructors

        public ClearModelDataAttribute(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }

        #endregion Public Constructors

        #region Private Properties

        private Type ViewModelType { get; set; }

        #endregion Private Properties

        #region Public Methods

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception is ValidationException == false)
            {
                var tempCookieKey = filterContext.HttpContext.Request.Cookies.Get(ConfigConst.TempCookieName);
                if (tempCookieKey != null)
                    filterContext.Controller.TempData.Remove(tempCookieKey.Value);
            }

            base.OnActionExecuted(filterContext);
        }

        #endregion Public Methods
    }
}