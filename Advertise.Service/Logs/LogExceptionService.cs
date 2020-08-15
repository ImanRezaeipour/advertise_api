using System;
using System.Web;
using Elmah;

namespace Advertise.Service.Logs
{
    public class LogExceptionService : ILogExceptionService
    {
        #region Public Methods

        public void Log(Exception exception)
        {
            ErrorLog.GetDefault(HttpContext.Current).Log(new Error(exception));
        }

        public void Raise(Exception exception)
        {
            ErrorSignal.FromCurrentContext().Raise(exception);
        }

        #endregion Public Methods
    }
}