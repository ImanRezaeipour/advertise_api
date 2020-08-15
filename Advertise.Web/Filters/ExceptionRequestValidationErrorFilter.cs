using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Web;
using System.Web.Mvc;
using Advertise.Core.Constants;
using Advertise.Core.Exceptions;
using Advertise.Service.Logs;
using Advertise.Web.Extensions;
using Advertise.Web.Results;
using Newtonsoft.Json;

namespace Advertise.Web.Filters
{
    public class ExceptionRequestValidationErrorFilter : IExceptionFilter
    {
        #region Private Fields

        private ILogExceptionService _exceptionLogService;

        public ExceptionRequestValidationErrorFilter(ILogExceptionService exceptionLogService)
        {
            _exceptionLogService = exceptionLogService;
        }

        #endregion Private Fields

        #region Public Properties

        //[SetterProperty]
        //public IExceptionLogService ExceptionLogService
        //{
        //    set { _exceptionLogService = value; }
        //}

        #endregion Public Properties

        #region Public Methods

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is HttpRequestValidationException)
            {
                HandleHttpRequestValidationException(context);
            }

            else if (context.Exception is ValidationException)
            {
                HandleValidationException(context);
            }

            else if (context.Exception is SeoException)
            {
                HandleSeoException(context);
            }

            else if (context.Exception is DbEntityValidationException)
            {
                HandleDbEntityValidationException(context);
            }

            else if (context.Exception is OptimisticConcurrencyException)
            {
                HandleOptimisticConcurrencyException(context);
            }

            else if (context.Exception is ServiceException)
            {
            }

            else if (context.Exception is JsonValidationException)
            {
                HandleJsonValidationException(context);
            }

            else if (context.Exception is NotImplementedException)
            {
            }

            else if (context.Exception is ArgumentNullException)
            {
            }

            else if (context.Exception is FactoryException)
            {
                HandleFactoryException(context);
            }

            else if (context.Exception != null)
            {
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void HandleDbEntityValidationException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            _exceptionLogService.Log(context.Exception);

            var dbEntityValidationException = context.Exception as DbEntityValidationException;
            if (dbEntityValidationException == null)
                return;
            foreach (var entityValidationError in dbEntityValidationException.EntityValidationErrors)
            {
                var entityType = entityValidationError.Entry.Entity.GetType().Name;
                var entityState = entityValidationError.Entry.State;
                foreach (var validationError in entityValidationError.ValidationErrors)
                {
                    // For See Validation Error
                    var propertyName = validationError.PropertyName;
                    var errorMessage = validationError.ErrorMessage;
                }
            }
        }

        private void HandleHttpRequestValidationException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            _exceptionLogService.Log(context.Exception);
        }

        private void HandleJsonValidationException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            _exceptionLogService.Log(context.Exception);

            context.Result = new JsonResult
            {
                Data = AjaxResult.Failed(AjaxErrorStatus.NoMoreInfo, context.Exception.Message),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        private void HandleOptimisticConcurrencyException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            _exceptionLogService.Log(context.Exception);

            var ctx = (this as IObjectContextAdapter).ObjectContext;
            ctx.Refresh(RefreshMode.ClientWins, ctx);
        }

        private void HandleSeoException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            _exceptionLogService.Log(context.Exception);

            context.HttpContext.Response.RedirectPermanent("/");
        }

        private void HandleValidationException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            _exceptionLogService.Log(context.Exception);

            if (context.HttpContext.Request.IsAjaxRequest())
            {
                if (context.Exception.Data.Contains("messages"))
                {
                    context.Result = new JsonResult
                    {
                        Data = AjaxResult.Failed(AjaxErrorStatus.ValidationError, JsonConvert.SerializeObject((IList<string>)context.Exception.Data["messages"])),
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            else
            {
                if (context.Exception.Data.Contains("messages"))
                {
                    foreach (var message in (IList<string>) context.Exception.Data["messages"])
                    {
                        context.Controller.ViewData.ModelState.AddModelError("", message);
                    }
                }
                else
                {
                    context.Controller.ViewData.ModelState.AddModelError("", context.Exception.Message);
                }

                if (context.HttpContext.Items.Contains("ActionName") /*context.Exception.Data.Contains("actionName")*/)
                {
                    context.Controller.MessageError(context.Exception.Message);
                    context.Result = new RedirectResult(context.HttpContext.Items["ActionName"].ToString() /*context.Exception.Data["actionName"].ToString()*/);
                }
                else if (context.HttpContext.Items.Contains("ViewName") /*context.Exception.Data.Contains("viewName")*/)
                {
                    var tempCookieKey = context.HttpContext.Request.Cookies.Get(ConfigConst.TempCookieName);
                    if (tempCookieKey != null)
                        context.Controller.ViewData.Model = context.Controller.TempData.Peek(tempCookieKey.Value);
                    context.Result = new ViewResult
                    {
                        ViewName = context.HttpContext.Items["ViewName"].ToString() /*context.Exception.Data["viewName"].ToString()*/,
                        ViewData = context.Controller.ViewData
                    };
                }
                else
                {
                    context.Result = new ViewResult
                    {
                        ViewData = context.Controller.ViewData
                    };
                }
            }


        }

        private void HandleFactoryException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            _exceptionLogService.Log(context.Exception);

            if (context.HttpContext.Request.IsAjaxRequest())
            {
                if (context.Exception.Data.Contains("messages"))
                {
                    context.Result = new JsonResult
                    {
                        Data = AjaxResult.Failed(AjaxErrorStatus.FactoryError,
                            JsonConvert.SerializeObject((IList<string>) context.Exception.Data["messages"])),
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            else
            {
                if (context.Exception.Data.Contains("messages"))
                {
                    foreach (var message in (IList<string>) context.Exception.Data["messages"])
                    {
                        context.Controller.ViewData.ModelState.AddModelError("", message);
                    }
                }
                else
                {
                    context.Controller.ViewData.ModelState.AddModelError("", context.Exception.Message);
                }

                context.Controller.MessageError(context.Exception.Message);
                context.Result = new RedirectResult("/NotFound");
            }
        }

        #endregion Private Methods
    }
}