using System;
using System.Runtime.Serialization;

namespace Advertise.Core.Exceptions
{
    public class FactoryException : Exception
    {
        #region Public Constructors

        public FactoryException()
        {
        }

        public FactoryException(string message) : base(message)
        {
        }

        public FactoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected FactoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Protected Constructors
    }
}