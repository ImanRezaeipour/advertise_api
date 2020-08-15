using System;
using System.Web;
using System.Web.Mvc;
using Advertise.Core.Constants;
using Advertise.Core.Helpers;

namespace Advertise.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ImportModelDataAttribute : ActionFilterAttribute
    {
        #region Public Constructors

        public ImportModelDataAttribute(Type viewModelType)
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
            var viewModel = filterContext.Controller.ViewData.Model;
            if (viewModel == null)
                return;

            if (viewModel.GetType() == ViewModelType)
            {
                var tempKeyGenerated = SequentialGuidGenerator.NewSequentialGuid();
                var httpCookie = new HttpCookie(ConfigConst.TempCookieName)
                {
                    Name = ConfigConst.TempCookieName,
                    Value = tempKeyGenerated.ToString(),
                    Expires = DateTime.Now.Add(TimeSpan.FromHours(6))
                };
                filterContext.HttpContext.Response.Cookies.Set(httpCookie);
                filterContext.Controller.TempData[tempKeyGenerated.ToString()] = viewModel;
            }

            base.OnActionExecuted(filterContext);
        }

        #endregion Public Methods
    }
}