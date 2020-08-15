using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Users;
using Advertise.Data.Validation.Common;
using Advertise.Service.Factory.Users;
using Advertise.Service.Services.Permissions;
using Advertise.Service.Users;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class UserAccountController : BaseController
    {
        #region Private Fields

        private readonly IUserAccountFactory _accountFactory;
        private readonly IUserAccountService _accountService;
        private readonly IUserService _userService;

        #endregion Private Fields

        #region Public Constructors

        public UserAccountController(IUserService userManager, IUserAccountService accountService, IUserAccountFactory accountFactory)
        {
            _userService = userManager;
            _accountService = accountService;
            _accountFactory = accountFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        public virtual async Task<HttpResponseMessage> ChangePassword(UserChangePasswordModel viewModel)
        {
            await _accountService.ChangePasswordByCurrentUserAsync(viewModel.OldPassword, viewModel.NewPassword);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        public virtual async Task<HttpResponseMessage> ConfirmationEmail(RequestModel model)
        {
            var result = await _userService.ConfirmEmailAsync(model.Id, model.Code.Base64ForUrlDecode());
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    
        [HttpPost]
        public virtual async Task<HttpResponseMessage> ConfirmationForgotPassword(UserForgotPasswordModel viewModel)
        {
            await _accountService.ConfirmForgotPasswordAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
   
        [HttpPost]
        public virtual async Task<HttpResponseMessage> ConfirmationPhoneNumber(UserVerifyPhoneNumberModel viewModel)
        {
            await _accountService.ConfirmPhoneNumberAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
       
        [HttpPost]
        public virtual async Task<HttpResponseMessage> ConfirmationResetPassword(UserResetPasswordModel viewModel)
        {
            await _accountService.ConfirmResetPasswordAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> EasyRegister(UserOperatorCreateModel viewModel)
        {
            await _accountService.RegisterEasyAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        public virtual async Task<HttpResponseMessage> GetEasyRegister()
        {
            var viewModel = await _accountFactory.PrepareUserOperatorCreateModel();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }
       
        [HttpGet]
        public virtual async Task<HttpResponseMessage> ExternalLoginCallback()
        {
            await _accountService.RegisterByExternalLinkAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
   
        [HttpPost]
        public virtual async Task<HttpResponseMessage> IsExistEmailAjax(RequestModel model)
        {
            var isEmailExist = await _userService.IsExistByEmailAsync(model.Email, model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, isEmailExist);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> IsExistUserNameAjax(RequestModel model)
        {
            var countEmailExist = await _userService.IsExistByUserNameAsync(model.UserName, model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, countEmailExist);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> RegisterByEmail(UserRegisterModel viewModel)
        {
            await _accountService.RegisterByEmailAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
     
        [HttpPost]
        public virtual async Task<HttpResponseMessage> RegisterByPhoneNumber(UserAddPhoneNumberModel viewModel)
        {
            await _accountService.RegisterByPhoneNumberAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> SetPassword(UserSetPasswordModel viewModel)
        {
            await _accountService.SetCurrentUserPasswordAsync(viewModel.NewPassword);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}