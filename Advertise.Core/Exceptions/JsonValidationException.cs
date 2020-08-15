using System;
using System.Runtime.Serialization;

namespace Advertise.Core.Exceptions
{
    public class JsonValidationException : Exception
    {
        #region Public Constructors

        public JsonValidationException()
        {
        }

        public JsonValidationException(string message) : base(message)
        {
        }

        public JsonValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected JsonValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Protected Constructors
    }
}