using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.WebPages;
using Advertise.Web.Utilities.Noty;
using Microsoft.AspNet.Identity;

namespace Advertise.Web.Extensions
{
    public static class ControllerExtensions
    {
        #region Public Methods

        public static void AddErrors(this System.Web.Mvc.Controller controller, IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                controller.ModelState.AddModelError("", error);
            }
        }

        public static void AddErrors(this System.Web.Mvc.Controller controller, string property, string error)
        {
            controller.ModelState.AddModelError(property, error);
        }

        public static string GetListOfErrors(this ModelStateDictionary modelState)
        {
            var list = modelState.ToList();
            return
                list.Select(keyValuePair => keyValuePair.Value.Errors.Select(a => a.ErrorMessage))
                    .Aggregate(string.Empty,
                        (current1, errors) =>
                            errors.Aggregate(current1, (current, error) => current + $"{error}\n"));
        }

        public static string GetUserManagerErros(this IEnumerable<string> errors)
        {
            return errors.Aggregate(string.Empty, (current, error) => current + $"{error} \n");
        }

        public static bool IsEmbeddedIntoAnotherDomain(this ControllerBase controller)
        {
            var url = controller.ControllerContext.HttpContext.Request.Url;
            var urlReferrer = controller.ControllerContext.HttpContext.Request.UrlReferrer;
            return url != null && (urlReferrer != null &&
                                   !url.Host.Equals(urlReferrer.Host,
                                       StringComparison.InvariantCultureIgnoreCase));
        }

        public static void MessageError(this ControllerBase controller, string message, bool isSticky = false)
        {
            var notyMessage = new NotyMessage
            {
                Type = NotyAlertType.Error,
                IsSticky = isSticky,
                Message = message
            };
            controller.AddNotyAlert(notyMessage);
        }

        public static void MessageSuccess(this ControllerBase controller, string message, bool isSticky = false)
        {
            var notyMessage = new NotyMessage
            {
                Type = NotyAlertType.Success,
                IsSticky = isSticky,
                Message = message,
                CloseWith = NotyMessageCloseType.Click,
                Location = NotyMessageLocationType.TopLeft,
                CloseAnimation = NotyAnimationType.BounceOut,
                OpenAnimation = NotyAnimationType.Bounce,
                IsModal = true
            };
            controller.AddNotyAlert(notyMessage);
        }

        public static void NotyAlert(this ControllerBase controller, string message, bool isSticky = false)
        {
            var notyMessage = new NotyMessage
            {
                Type = NotyAlertType.Alert,
                IsSticky = isSticky,
                Message = message
            };
            controller.AddNotyAlert(notyMessage);
        }

        public static void NotyInformation(this ControllerBase controller, string message, bool isSticky = false)
        {
            var notyMessage = new NotyMessage
            {
                Type = NotyAlertType.Information,
                IsSticky = isSticky,
                Message = message
            };
            controller.AddNotyAlert(notyMessage);
        }

        public static void NotySuccessModal(this ControllerBase controller, string message, bool isSticky = false, NotyMessageLocationType location = NotyMessageLocationType.Center)
        {
            var notyMessage = new NotyMessage
            {
                Type = NotyAlertType.Success,
                IsSticky = isSticky,
                Message = message,
                Location = location,
                CloseAnimation = NotyAnimationType.BounceIn,
                OpenAnimation = NotyAnimationType.BounceOut,
                IsModal = true,
                CloseWith = NotyMessageCloseType.Click
            };
            controller.AddNotyAlert(notyMessage);
        }

        public static void NotyWarning(this ControllerBase controller, string message, bool isSticky = false)
        {
            var notyMessage = new NotyMessage
            {
                Type = NotyAlertType.Warning,
                IsSticky = isSticky,
                Message = message
            };
            controller.AddNotyAlert(notyMessage);
        }

        public static string RenderPartialViewToString(this ControllerBase controller)
        {
            return RenderPartialViewToString(controller, null, null);
        }

        public static string RenderPartialViewToString(this ControllerBase controller, string viewName)
        {
            return RenderPartialViewToString(controller, viewName, null);
        }

        public static string RenderPartialViewToString(this ControllerBase controller, object model)
        {
            return RenderPartialViewToString(controller, null, model);
        }

        public static string RenderPartialViewToString(this ControllerBase controller, string viewName, object model)
        {
            if (viewName.IsEmpty())
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;

            try
            {
                using (var sw = new System.IO.StringWriter())
                {
                    var viewResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);

                    ThrowIfViewNotFound(viewResult, viewName);

                    var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);

                    return sw.GetStringBuilder().ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RenderViewToString(this ControllerBase controller)
        {
            return RenderViewToString(controller, null, null, null);
        }

        public static string RenderViewToString(this ControllerBase controller, object model)
        {
            return RenderViewToString(controller, null, null, model);
        }

        public static string RenderViewToString(this ControllerBase controller, string viewName)
        {
            return RenderViewToString(controller, viewName, null, null);
        }

        public static string RenderViewToString(this ControllerBase controller, string viewName, string masterName)
        {
            return RenderViewToString(controller, viewName, masterName, null);
        }

        public static string RenderViewToString(this ControllerBase controller, string viewName, string masterName, object model)
        {
            if (viewName.IsEmpty())
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;

            using (var sw = new System.IO.StringWriter())
            {
                var viewResult = System.Web.Mvc.ViewEngines.Engines.FindView(controller.ControllerContext, viewName, masterName);

                ThrowIfViewNotFound(viewResult, viewName);

                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public static string RenderViewToString(this ControllerContext context,
            string viewPath,
            object model = null,
            bool partial = false)
        {
            ViewEngineResult viewEngineResult = null;
            viewEngineResult = partial
                ? System.Web.Mvc.ViewEngines.Engines.FindPartialView(context, viewPath)
                : System.Web.Mvc.ViewEngines.Engines.FindView(context, viewPath, null);
            if (viewEngineResult == null) throw new FileNotFoundException("View cannot be found.");
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;
            string result = null;
            using (var sw = new System.IO.StringWriter())
            {
                var ctx = new ViewContext(context, view, context.Controller.ViewData, context.Controller.TempData, sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }
            return result;
        }

        #endregion Public Methods

        #region Private Methods

        private static void AddNotyAlert(this ControllerBase controller, NotyMessage message)
        {
            var noty = controller.TempData.ContainsKey(Utilities.Noty.NotyAlert.TempDataKey)
                 ? (NotyAlert)controller.TempData[Utilities.Noty.NotyAlert.TempDataKey]
                 : new NotyAlert();

            noty.AddNotyMessage(message);

            controller.TempData[Utilities.Noty.NotyAlert.TempDataKey] = noty;
        }

        private static void ThrowIfViewNotFound(ViewEngineResult viewResult, string viewName)
        {
            // if view not found, throw an exception with searched locations
            if (viewResult.View != null) return;
            var locations = new StringBuilder();
            locations.AppendLine();

            foreach (var location in viewResult.SearchedLocations)
            {
                locations.AppendLine(location);
            }

            throw new InvalidOperationException(string.Format("The view '{0}' or its master was not found, searched locations: {1}", viewName, locations));
        }

        #endregion Private Methods
    }
}