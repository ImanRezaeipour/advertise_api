using System;
using System.Threading.Tasks;
using Advertise.Core.Domain.Users;
using Advertise.Core.Model.Users;
using Microsoft.AspNet.Identity.Owin;

namespace Advertise.Service.Users
{
    public interface IUserAccountService
    {
        Task ChangePasswordByCurrentUserAsync(string oldPassword, string newPassword);
        Task ConfirmForgotPasswordAsync(UserForgotPasswordModel viewModel);
        Task ConfirmPhoneNumberAsync(UserVerifyPhoneNumberModel viewModel);
        Task ConfirmResetPasswordAsync(UserResetPasswordModel viewModel);
        Task<SignInStatus> PasswordSignInAsync(UserLoginModel viewModel);
        Task RegisterByEmailAsync(UserRegisterModel model);
        Task RegisterByExternalLinkAsync();
        Task RegisterByPhoneNumberAsync(UserAddPhoneNumberModel viewModel);
        Task RegisterEasyAsync(UserOperatorCreateModel model);
        Task  SendEmailConfirmationTokenAsync(Guid userId);
        Task SendForgotPasswordConfirmationTokenAsync(Guid userId);
        Task SendPhoneNumberConfirmationTokenAsync(Guid userId, string phoneNumber);
        Task SetCurrentUserPasswordAsync(string password);
        Task<SignInStatus> SignInByIdAsync(Guid id);
        Task SignOutCurrentUserAsync();
        Task UpdateAuditFieldsAsync(User user, Guid auditUserId);
    }
}