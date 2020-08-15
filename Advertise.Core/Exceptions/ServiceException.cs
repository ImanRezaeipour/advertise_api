using System;
using System.Runtime.Serialization;

namespace Advertise.Core.Exceptions
{
    public class ServiceException : Exception
    {
        #region Public Constructors

        public ServiceException()
        {
        }

        public ServiceException(string message) : base(message)
        {
        }

        public ServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected ServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Protected Constructors
    }
}