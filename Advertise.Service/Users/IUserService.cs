using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Advertise.Core.Domain.Locations;
using Advertise.Core.Domain.Users;
using Advertise.Core.Model.Users;
using Advertise.Core.Objects;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;

namespace Advertise.Service.Users
{
    public interface IUserService
    {
        bool AutoCommitEnabled { get; set; }
        IPasswordHasher PasswordHasher { get; set; }
        IIdentityValidator<User> UserValidator { get; set; }
        IIdentityValidator<string> PasswordValidator { get; set; }
        IClaimsIdentityFactory<User, Guid> ClaimsIdentityFactory { get; set; }
        IIdentityMessageService EmailService { get; set; }
        IIdentityMessageService SmsService { get; set; }
        IUserTokenProvider<User, Guid> UserTokenProvider { get; set; }
        bool UserLockoutEnabledByDefault { get; set; }
        int MaxFailedAccessAttemptsBeforeLockout { get; set; }
        TimeSpan DefaultAccountLockoutTimeSpan { get; set; }
        bool SupportsUserTwoFactor { get; }
        bool SupportsUserPassword { get; }
        bool SupportsUserSecurityStamp { get; }
        bool SupportsUserRole { get; }
        bool SupportsUserLogin { get; }
        bool SupportsUserEmail { get; }
        bool SupportsUserPhoneNumber { get; }
        bool SupportsUserClaim { get; }
        bool SupportsUserLockout { get; }
        bool SupportsQueryableUsers { get; }
        IQueryable<User> Users { get; }
        IDictionary<string, IUserTokenProvider<User, Guid>> TwoFactorProviders { get; }
        Task AddToRoleByIdAsync(Guid userId, UserRole userRole);
        Task<int> CountAllAsync();
        Task<int> CountByRequestAsync(UserSearchModel model);
        Task CreateByViewModelAsync(UserCreateModel model);
        Task CreateUserMetaByUserIdAsync(Guid userId);
        Task CreateUserMetaByViewModelAsync(UserCreateModel model);
        Task DeleteByIdAsync(Guid userId);
        Task EditByViewModelAsync(UserEditModel model);
        Task EditMetaByViewModelAsync(UserEditMeModel model, bool isCurrentUser = false);
        Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken));
        Task<User> FindByPhoneNumberAsync(string phoneNumber);
        Task<User> FindByUserIdAsync(Guid userId, bool isCurrentUser = false);
        Task<User> FindByUserNameAsync(string username);
        Task<UserMeta> FindUserMetaByCurrentUserAsync();
        Task<ClaimsIdentity> GenerateUserIdentityAsync(UserService service, User user);
        Task<string> GenerateUserNameAsync();
        Task<Location> GetAddressByIdAsync(Guid userId);
        Task<User> GetByEmailAndPasswordAsync(string email, string password, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> IsExistByEmailAndPasswordAsync(string email, string password, CancellationToken cancellationToken = default(CancellationToken));
        Task<User> GetCurrentUserAsync();
        Task<string> GetCurrentUserNameAsync();
        IUserEmailStore<User, Guid> GetEmailStore();
        Task<IList<FineUploaderObject>> GetFileAsFineUploaderModelByIdAsync(Guid userId);
        Task<IList<string>> GetPhoneNumbersByUserIdsAsync(IList<Guid?> userIds);
        Task<IList<string>> GetRolesAsync(Guid userId);
        Task<User> GetSystemUserAsync();
        Task<UserMeta> GetUserMetaByIdAsync(Guid userId);
        Task<IList<User>> GetUsersByRequestAsync(UserSearchModel model);
        Task<IList<User>> GetUsersByRoleIdAsync(Guid roleId);
        Task<bool> HasUserNameByCurrentUserAsync();
        Task<bool> IsBanByIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<bool> IsBanByUserNameAsync(string userName);
        Task<bool> IsEmailConfirmedByEmailAsync(string email, HttpContext httpContext, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> IsExistByEmailAsync(string email, Guid? userId = null );
        Task<bool> IsExistByEmailCancellationTokenAsync(string email,  CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> IsExistByIdCancellationTokenAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> IsExistByUserNameAsync(string userName, Guid? userId= null, Guid? exceptUserId = null);
        Task<bool> IsExistByUserNameCancellationTokenAsync(string userName, CancellationToken cancellationToken);
        Task<bool> IsLockedOutAsync(Guid userId, CancellationToken cancellationToken);
        Task<bool> IsLockedOutAsync(string email, CancellationToken cancellationToken);
        Task<bool> IsBanByEmailAsync(string email, CancellationToken cancellationToken);
        Task<string> MaxByRequestAsync(UserSearchModel model, string aggregateMember);
        Func<CookieValidateIdentityContext, Task> OnValidateIdentity();
        IQueryable<User> QueryByRequest(UserSearchModel model);
        void SeedDatabase();
        void UserManagerOptions();
        void Dispose();
        Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType);
        Task<IdentityResult> CreateAsync(User user);
        Task<IdentityResult> UpdateAsync(User user);
        Task<IdentityResult> DeleteAsync(User user);
        Task<User> FindByIdAsync(Guid userId);
        Task<User> FindByNameAsync(string userName);
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<User> FindAsync(string userName, string password);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<bool> HasPasswordAsync(Guid userId);
        Task<IdentityResult> AddPasswordAsync(Guid userId, string password);
        Task<IdentityResult> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
        Task<IdentityResult> RemovePasswordAsync(Guid userId);
        Task<string> GetSecurityStampAsync(Guid userId);
        Task<IdentityResult> UpdateSecurityStampAsync(Guid userId);
        Task<string> GeneratePasswordResetTokenAsync(Guid userId);
        Task<IdentityResult> ResetPasswordAsync(Guid userId, string token, string newPassword);
        Task<User> FindAsync(UserLoginInfo login);
        Task<IdentityResult> RemoveLoginAsync(Guid userId, UserLoginInfo login);
        Task<IdentityResult> AddLoginAsync(Guid userId, UserLoginInfo login);
        Task<IList<UserLoginInfo>> GetLoginsAsync(Guid userId);
        Task<IdentityResult> AddClaimAsync(Guid userId, Claim claim);
        Task<IdentityResult> RemoveClaimAsync(Guid userId, Claim claim);
        Task<IList<Claim>> GetClaimsAsync(Guid userId);
        Task<IdentityResult> AddToRoleAsync(Guid userId, string role);
        Task<IdentityResult> AddToRolesAsync(Guid userId, params string[] roles);
        Task<IdentityResult> RemoveFromRolesAsync(Guid userId, params string[] roles);
        Task<IdentityResult> RemoveFromRoleAsync(Guid userId, string role);
        Task<bool> IsInRoleAsync(Guid userId, string role);
        Task<string> GetEmailAsync(Guid userId);
        Task<IdentityResult> SetEmailAsync(Guid userId, string email);
        Task<User> FindByEmailAsync(string email);
        Task<string> GenerateEmailConfirmationTokenAsync(Guid userId);
        Task<IdentityResult> ConfirmEmailAsync(Guid userId, string token);
        Task<bool> IsEmailConfirmedAsync(Guid userId);
        Task<string> GetPhoneNumberAsync(Guid userId);
        Task<IdentityResult> SetPhoneNumberAsync(Guid userId, string phoneNumber);
        Task<IdentityResult> ChangePhoneNumberAsync(Guid userId, string phoneNumber, string token);
        Task<bool> IsPhoneNumberConfirmedAsync(Guid userId);
        Task<string> GenerateChangePhoneNumberTokenAsync(Guid userId, string phoneNumber);
        Task<bool> VerifyChangePhoneNumberTokenAsync(Guid userId, string token, string phoneNumber);
        Task<bool> VerifyUserTokenAsync(Guid userId, string purpose, string token);
        Task<string> GenerateUserTokenAsync(string purpose, Guid userId);
        void RegisterTwoFactorProvider(string twoFactorProvider, IUserTokenProvider<User, Guid> provider);
        Task<IList<string>> GetValidTwoFactorProvidersAsync(Guid userId);
        Task<bool> VerifyTwoFactorTokenAsync(Guid userId, string twoFactorProvider, string token);
        Task<string> GenerateTwoFactorTokenAsync(Guid userId, string twoFactorProvider);
        Task<IdentityResult> NotifyTwoFactorTokenAsync(Guid userId, string twoFactorProvider, string token);
        Task<bool> GetTwoFactorEnabledAsync(Guid userId);
        Task<IdentityResult> SetTwoFactorEnabledAsync(Guid userId, bool enabled);
        Task SendEmailAsync(Guid userId, string subject, string body);
        Task SendSmsAsync(Guid userId, string message);
        Task<bool> IsLockedOutAsync(Guid userId);
        Task<IdentityResult> SetLockoutEnabledAsync(Guid userId, bool enabled);
        Task<bool> GetLockoutEnabledAsync(Guid userId);
        Task<DateTimeOffset> GetLockoutEndDateAsync(Guid userId);
        Task<IdentityResult> SetLockoutEndDateAsync(Guid userId, DateTimeOffset lockoutEnd);
        Task<IdentityResult> AccessFailedAsync(Guid userId);
        Task<IdentityResult> ResetAccessFailedCountAsync(Guid userId);
        Task<int> GetAccessFailedCountAsync(Guid userId);
        Guid CurrentRoleId { get; }
        Task<User> FindByPhoneNumberAsync(string phoneNumber, string password);
        Task<bool> IsExistByPhoneNumberAsync(string phoneNumber);
    }
}