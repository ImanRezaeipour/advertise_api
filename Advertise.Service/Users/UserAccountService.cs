using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using Advertise.Core.Configuration;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Domain.Locations;
using Advertise.Core.Domain.Roles;
using Advertise.Core.Domain.Users;
using Advertise.Core.Exceptions;
using Advertise.Core.Helpers;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Companies;
using Advertise.Core.Model.Users;
using Advertise.Core.Types;
using Advertise.Data.DbContexts;
using Advertise.Data.Validation.Common;
using Advertise.Service.Categories;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using Advertise.Service.Locations;
using Advertise.Service.Plans;
using Advertise.Service.Roles;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Advertise.Service.Users
{
    public class UserAccountService : IUserAccountService
    {
        #region Private Fields

        private readonly IAuthenticationManager _authenticationManager;
        private readonly ICompanyService _companyService;
        private readonly IConfigurationManager _configurationManager;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IPlanService _planService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<UserMeta> _userMetaRepository;
        private readonly IUserOperationServive _userOperationServive;
        private readonly IUserService _userService;
        private readonly IUserSettingService _userSettingService;
        private readonly IWebContextManager _webContextManager;
        private readonly IModelValidator _modelValidator;
        private readonly ILocationService _locationService;
        private readonly ILocationCityService _locationCityService;
        private readonly ICategoryService _categoryService;
        private readonly IRoleService _roleService;

        #endregion Private Fields

        #region Public Constructors

        public UserAccountService(IUserService userService, IConfigurationManager configurationManager, IMapper mapper, ICompanyService companyService, IUserSettingService userSettingService, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IPlanService planService, IUserOperationServive userOperationServive, IEventPublisher eventPublisher, IAuthenticationManager authenticationManager, IModelValidator modelValidator, ILocationService locationService, ILocationCityService locationCityService, ICategoryService categoryService, IRoleService roleService)
        {
            _userService = userService;
            _configurationManager = configurationManager;
            _mapper = mapper;
            _companyService = companyService;
            _userSettingService = userSettingService;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _planService = planService;
            _userOperationServive = userOperationServive;
            _eventPublisher = eventPublisher;
            _authenticationManager = authenticationManager;
            _modelValidator = modelValidator;
            _locationService = locationService;
            _locationCityService = locationCityService;
            _categoryService = categoryService;
            _roleService = roleService;
            _userMetaRepository = unitOfWork.Set<UserMeta>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task ChangePasswordByCurrentUserAsync(string oldPassword, string newPassword)
        {
            await _userService.ChangePasswordAsync(_webContextManager.CurrentUserId, oldPassword, newPassword);

            var user = await _userService.FindByIdAsync(_webContextManager.CurrentUserId);

            _authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await _userService.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            _authenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = false
            }, identity);
        }

        public async Task ConfirmForgotPasswordAsync(UserForgotPasswordModel viewModel)
        {
            if (viewModel.Email == null)
                throw new ArgumentNullException(nameof(viewModel.Email));

            var user = await _userService.FindByEmailAsync(viewModel.Email);
            if (user == null)
                throw new ServiceException();

            await SendForgotPasswordConfirmationTokenAsync(user.Id);
        }

        public async Task ConfirmPhoneNumberAsync(UserVerifyPhoneNumberModel viewModel)
        {
            _modelValidator.Validate<ObjectValidator, object>(viewModel);
            
            var user = await _userService.FindByPhoneNumberAsync(viewModel.PhoneNumber);
          
            var changePhoneNumberResult = await _userService.ChangePhoneNumberAsync(user.Id, viewModel.PhoneNumber, viewModel.Code);
           
            var removePasswordResult = await _userService.RemovePasswordAsync(user.Id);
           
            var addPasswordResult = await _userService.AddPasswordAsync(user.Id, viewModel.Password);
           
            var updateSecurityStampResult = await _userService.UpdateSecurityStampAsync(user.Id);
        }

        public async Task ConfirmResetPasswordAsync(UserResetPasswordModel viewModel)
        {
            viewModel.Code = viewModel.Code.Replace(" ", "+");
            var user = await _userService.FindByEmailAsync(viewModel.Email);
            await _userService.ResetPasswordAsync(user.Id, viewModel.Code, viewModel.Password);
        }

        public async Task<SignInStatus> PasswordSignInAsync(UserLoginModel viewModel)
        {
            var result = viewModel.UserName.StartsWith(CodeConst.MyApp);
            if (result)
                return SignInStatus.Failure;

            var user = await _userService.FindByEmailAsync(viewModel.UserName);
            if (user == null)
                throw new ServiceException();
            viewModel.UserName = user.UserName;

            var trust = await _userService.CheckPasswordAsync(user, viewModel.Password);
            if (!trust)
                return SignInStatus.Failure;

            var identity = await _userService.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            _authenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = viewModel.RememberMe
            }, identity);

            await _userService.SendSmsAsync(user.Id, $"{user.UserName} عزیز به سامانه نویناک خوش آمدید. آخرین ورود شما {user.LastLoginedOn}");
            return SignInStatus.Success;
        }

        public async Task RegisterByEmailAsync(UserRegisterModel model)
        {
            var exist = await _userService.IsExistByEmailAsync(model.Email);
            if (exist)
                throw new Exception("The email address is exist.");
            
            var defaultLocation = await _locationService.GetDefaultLocationAsync();
            var superUser = await _userService.GetSystemUserAsync();
            var userRole = await _roleService.FindByNameAsync("Users");
            
            var user = _mapper.Map<User>(model);
            user.UserName = await _userService.GenerateUserNameAsync();
            user.Id = SequentialGuidGenerator.NewSequentialGuid();
            user.CreatedOn = DateTime.Now;
            user.Version = 1;
            user.CreatedById = superUser.Id;
            user.Meta = new UserMeta
            {
                Id = SequentialGuidGenerator.NewSequentialGuid(),
                IsActive = true,
                Location = new Location
                {
                    Id = SequentialGuidGenerator.NewSequentialGuid(),
                }
            };
            user.Company = new Company
            {
                Id = SequentialGuidGenerator.NewSequentialGuid(),
                State = StateType.Approved,
                CategoryId = (await _categoryService.GetRootAsync()).Id,
                CreatedOn = DateTime.Now,
                Location = new Location
                {
                    Id = SequentialGuidGenerator.NewSequentialGuid(),
                    Latitude = defaultLocation.Item1,
                    Longitude = defaultLocation.Item2,
                    CityId = await _locationCityService.GetIdByNameAsync(defaultLocation.Item3),
                }
            };
            user.Roles.Add(new UserRole
            {
                RoleId = userRole.Id,
            });
            user.Setting = new UserSetting
            {
                Id = SequentialGuidGenerator.NewSequentialGuid(),
                Language = LanguageType.Persian,
                Theme = ThemeType.White
            };
            
            var result = await _userService.CreateAsync(user, model.Email);
            if (result.Succeeded)
                await SendEmailConfirmationTokenAsync(user.Id);
        }

        public async Task RegisterByExternalLinkAsync()
        {
            var externalLoginInfo = await _authenticationManager.GetExternalLoginInfoAsync();

            await _userService.IsExistByEmailAsync(externalLoginInfo.Email);

            var user = _mapper.Map<User>(new UserRegisterModel());
            user.Email = externalLoginInfo.Email;
            var superUser = await _userService.GetSystemUserAsync();
            await UpdateAuditFieldsAsync(user, superUser.Id);

            var userSaved = await _userService.FindByEmailAsync(externalLoginInfo.Email);
            await _userService.AddToRoleAsync(userSaved.Id, "کاربران");
            await _userService.CreateUserMetaByUserIdAsync(userSaved.Id);
            await _companyService.CreateByUserIdAsync(userSaved.Id);
            user.MetaId = (await _userMetaRepository.FirstOrDefaultAsync(model => model.CreatedById == user.Id)).Id;
            user.CompanyId = (await _companyService.FindByIdAsync(userSaved.Id)).Id;

            await _unitOfWork.SaveAllChangesAsync(auditUserId: user.Id);

            await SendEmailConfirmationTokenAsync(userSaved.Id);
        }

        public async Task RegisterByPhoneNumberAsync(UserAddPhoneNumberModel model)
        {
            var exist = await _userService.IsExistByPhoneNumberAsync(model.PhoneNumber);
            if (exist)
                throw new Exception("The phone number is exist.");
            
            var defaultLocation = await _locationService.GetDefaultLocationAsync();
            var superUser = await _userService.GetSystemUserAsync();
            var userRole = await _roleService.FindByNameAsync("Users");
            
            var user = _mapper.Map<User>(model);
            user.UserName = await _userService.GenerateUserNameAsync();
            user.Id = SequentialGuidGenerator.NewSequentialGuid();
            user.CreatedOn = DateTime.Now;
            user.Version = 1;
            user.CreatedById = superUser.Id;
            user.Meta = new UserMeta
            {
                Id = SequentialGuidGenerator.NewSequentialGuid(),
                IsActive = true,
                Location = new Location
                {
                    Id = SequentialGuidGenerator.NewSequentialGuid(),
                }
            };
            user.Company = new Company
            {
                Id = SequentialGuidGenerator.NewSequentialGuid(),
                State = StateType.Approved,
                CategoryId = (await _categoryService.GetRootAsync()).Id,
                CreatedOn = DateTime.Now,
                Location = new Location
                {
                    Id = SequentialGuidGenerator.NewSequentialGuid(),
                    Latitude = defaultLocation.Item1,
                    Longitude = defaultLocation.Item2,
                    CityId = await _locationCityService.GetIdByNameAsync(defaultLocation.Item3),
                }
            };
            user.Roles.Add(new UserRole
            {
                RoleId = userRole.Id,
            });
            user.Setting = new UserSetting
            {
                Id = SequentialGuidGenerator.NewSequentialGuid(),
                Language = LanguageType.Persian,
                Theme = ThemeType.White
            };
            
            var result = await _userService.CreateAsync(user, model.PhoneNumber);
            if (result.Succeeded)
                await SendPhoneNumberConfirmationTokenAsync(user.Id, model.PhoneNumber);
        }

        public async Task RegisterEasyAsync(UserOperatorCreateModel model)
        {
            //  CREATE USER
            var user = _mapper.Map<User>(model);
            user.UserName = await _userService.GenerateUserNameAsync();
            user.EmailConfirmed = true;
            user.IsBan = false;
            await UpdateAuditFieldsAsync(user, _webContextManager.CurrentUserId);
            await _userService.CreateAsync(user, model.Password);

            //  UPDATE USER ROLE
            var userSaved = await _userService.FindByEmailAsync(model.Email);
            var role = await _planService.FindByIdAsync(model.RoleId.GetValueOrDefault());
            var userRole = new UserRole
            {
                UserId = userSaved.Id,
                RoleId = role.RoleId.GetValueOrDefault()
            };
            await _userService.AddToRoleByIdAsync(userSaved.Id, userRole);

            //  CREATE COMPANY
            var companyViewModel = new CompanyCreateModel
            {
                Alias = model.Alias,
                Email = model.Email,
                MobileNumber = model.MobileNumber,
                Title = model.CompanyTitle,
                CategoryId = model.CategoryId.GetValueOrDefault(),
                CreatedById = userSaved.Id
            };
            await _companyService.CreateEasyByViewModelAsync(companyViewModel);

            //  CREATE USER META
            var userMetaViewModel = new UserCreateModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreatedById = userSaved.Id
            };
            await _userService.CreateUserMetaByViewModelAsync(userMetaViewModel);

            //  UPDATE USER META
            user.MetaId = (await _userMetaRepository.FirstOrDefaultAsync(m => m.CreatedById == user.Id)).Id;
            user.CompanyId = (await _companyService.FindByUserIdAsync(userSaved.Id)).Id;

            //  CREATE USER OPERATION
            var userOperator = new UserOperator
            {
                Amount = model.Amount,
                Description = model.Description,
                CreatedById = userSaved.Id
            };
            await _userOperationServive.CreateByModelAsync(userOperator);

            await _unitOfWork.SaveAllChangesAsync(auditUserId: userSaved.Id);
            _eventPublisher.EntityInserted(user);
        }

        public async Task  SendEmailConfirmationTokenAsync(Guid userId)
        {
            var code = await _userService.GenerateEmailConfirmationTokenAsync(userId);
            var url = HttpContext.Current.Request.Url;
            var callbackUrl = $"{url.Scheme}://{url.Authority}/{_configurationManager.ConfirmationEmailUrl}?id={userId}&code={code.Base64ForUrlEncode()}";
            var subject = "Email Confirmation";
            var body = "<span>" +
                       $"Please click <a href='{callbackUrl}'>here</a> to confirm your email." +
                       "</span>" +
                       "<hr/>" +
                       "<br/>" +
                       "<span>If above link doesn't work, copy above link manually and paste it on your browser.</span>" +
                       "<br/><br/><br/>" +
                       $"<span>{callbackUrl}</span>";

            await _userService.SendEmailAsync(userId, subject, body);
        }

        public async Task SendForgotPasswordConfirmationTokenAsync(Guid userId)
        {
            var code = await _userService.GeneratePasswordResetTokenAsync(userId);
            var url = HttpContext.Current.Request.Url;
            var callbackUrl = $"{url.Scheme}://{url.Authority}/{_configurationManager.ConfirmationResetPasswordUrl}?userid={userId}&code={code}";
            var subject = "بازیابی کلمه عبور";
            var body = "<span>" +
                       "لطفا جهت بازیابی پسورد خود" +
                       $" <a href='{callbackUrl}'>اینجا کلیک کنید</a>" +
                       "</span>" +
                       "<hr/>" +
                       "<br/>".Repeat(2) +
                       "<span> در صورتی که لینک بالا کار نکرد ، لینک زیر را به صورت دستی در مرورگر خود وارد کنید</span>" +
                       "<br/>".Repeat(2) +
                       $"<span>{callbackUrl}</span>";

            await _userService.SendEmailAsync(userId, subject, body);
        }

        public async Task SendPhoneNumberConfirmationTokenAsync(Guid userId, string phoneNumber)
        {
            if (phoneNumber == null)
                return;

            var code = await _userService.GenerateChangePhoneNumberTokenAsync(userId, phoneNumber);
            if (_userService.SmsService == null)
                return;

            var message = new IdentityMessage
            {
                Destination = phoneNumber,
                Body = "Security code is: " + code
            };
            await _userService.SmsService.SendAsync(message);
        }

        public async Task SetCurrentUserPasswordAsync(string password)
        {
            await _userService.AddPasswordAsync(_webContextManager.CurrentUserId, password);
            var user = await _userService.FindByIdAsync(_webContextManager.CurrentUserId);
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await _userService.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);
        }

        public async Task<SignInStatus> SignInByIdAsync(Guid id)
        {
            var user = await _userService.FindByIdAsync(id);
            if (user == null)
                return SignInStatus.Failure;

            var identity = await _userService.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);

            return SignInStatus.Success;
        }

        public async Task SignOutCurrentUserAsync()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public async Task UpdateAuditFieldsAsync(User user, Guid auditUserId)
        {
            user.Id = SequentialGuidGenerator.NewSequentialGuid();
            user.CreatedOn = DateTime.Now;
            //user.ModifiedOn = auditDate;
            //user.Audit = AuditType.Create;
           // user.CreatorIp = auditUserIp;
           // user.ModifierIp = auditUserIp;
           // user.CreatorAgent = auditUserAgent;
           // user.ModifierAgent = auditUserAgent;
            user.Version = 1;
            user.CreatedById = auditUserId;
            //user.ModifiedById = auditUserId;
        }

        #endregion Public Methods
    }
}