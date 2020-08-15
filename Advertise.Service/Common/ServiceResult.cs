using System.Collections.Generic;

namespace Advertise.Service.Common
{
    public class ServiceResult
    {
        #region Private Fields

        private static readonly ServiceResult _success = new ServiceResult { Succeeded = true };
        private List<string> _errors = new List<string>();

        #endregion Private Fields

        #region Public Properties

        public IEnumerable<string> Errors => _errors;

        public bool Succeeded { get; protected set; }

        public static ServiceResult Success => _success;

        #endregion Public Properties

        #region Public Methods

        public static ServiceResult Failed(params string[] errors)
        {
            var result = new ServiceResult { Succeeded = false };
            if (errors != null)
            {
                result._errors.AddRange(errors);
            }
            return result;
        }

        #endregion Public Methods
    }
}