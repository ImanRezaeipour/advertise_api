using System.Web.Mvc;
using Advertise.Service.Logs;

namespace Advertise.Web.Filters
{
    public class ExceptionHandledErrorLoggerFilter : IExceptionFilter
    {
        #region Private Fields

        private ILogExceptionService _exceptionLogService;

        public ExceptionHandledErrorLoggerFilter(ILogExceptionService exceptionLogService)
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

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                _exceptionLogService.Raise(filterContext.Exception);
        }

        #endregion Public Methods
    }
}