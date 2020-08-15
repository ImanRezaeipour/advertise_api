using System.Web.Mvc;
using Advertise.Core.Constants;

namespace Advertise.Web.Results
{
    public class AjaxResult:ActionResult
    {
        #region Public Properties

        public object Data { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }

        #endregion Public Properties

        #region Public Methods

        public static AjaxResult Failed(AjaxErrorStatus errorStatus, string errorMessage = null)
        {
            var result = new AjaxResult { Success = false, Data = null, ErrorCode = (int)errorStatus, ErrorMessage = errorMessage };
            return result;
        }

        public static AjaxResult Succeeded(object data = null)
        {
            var result = new AjaxResult { Success = true, Data = data, ErrorCode = (int)AjaxErrorStatus.None, ErrorMessage = "" };
            return result;
        }

        #endregion Public Methods

        public override void ExecuteResult(ControllerContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}