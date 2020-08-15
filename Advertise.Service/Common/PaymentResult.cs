using System.Collections.Generic;

namespace Advertise.Service.Common
{
    public class PaymentResult
    {
        #region Private Fields

        private static readonly PaymentResult _success = new PaymentResult { Succeeded = true };
        private List<string> _errors = new List<string>();

        #endregion Private Fields

        #region Public Properties

        public IEnumerable<string> Errors => _errors;

        public bool Succeeded { get; protected set; }

        public string ReturnUrl { get; set; }

        public static PaymentResult Success => _success;

        #endregion Public Properties

        #region Public Methods

        public static PaymentResult Failed(params string[] errors)
        {
            var result = new PaymentResult { Succeeded = false };
            if (errors != null)
            {
                result._errors.AddRange(errors);
            }
            return result;
        }

        public static PaymentResult Succeed(string returnUrl)
        {
            var result = new PaymentResult { Succeeded = true, ReturnUrl = returnUrl};

            return result;
        }

        #endregion Public Methods
    }
}