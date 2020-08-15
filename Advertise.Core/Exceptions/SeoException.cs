using System;

namespace Advertise.Core.Exceptions
{
    public sealed class SeoException : Exception
    {
        #region Public Constructors

        public SeoException(string message) : base(message)
        {
        }

        #endregion Public Constructors
    }
}