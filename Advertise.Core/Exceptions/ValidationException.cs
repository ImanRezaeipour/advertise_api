using System;
using System.Collections.Generic;

namespace Advertise.Core.Exceptions
{
    public sealed class ValidationException : Exception
    {
        #region Public Constructors

        public ValidationException(string message, string viewName = null, string actionName = null) : base(message)
        {
            if (viewName != null)
                Data.Add("viewName", viewName);

            if (actionName != null)
                Data.Add("actionName", actionName);
        }

        public ValidationException(IList<string> messages, string viewName = null, string actionName = null) : base()
        {
            if(messages != null)
                Data.Add("messages", messages);

            if (viewName != null)
                Data.Add("viewName", viewName);

            if (actionName != null)
                Data.Add("actionName", actionName);
        }

        #endregion Public Constructors

        #region Public Properties

        public string ActionName { get; set; }
        public IList<string> Messages { get; set; }
        public string ViewName { get; set; }

        #endregion Public Properties
    }
}