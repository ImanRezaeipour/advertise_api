using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Advertise.Core.Common;
using Advertise.Core.Configuration;
using Advertise.Core.Domain.Locations;
using Advertise.Core.Domain.Users;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.File;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Users;
using Advertise.Data.DbContexts;
using Advertise.Data.Validation.Common;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using Advertise.Service.Locations;
using Advertise.Service.Products;
using Advertise.Service.Roles;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using FineUploaderObject = Advertise.Core.Objects.FineUploaderObject;

namespace Advertise.Service.Users
{
    public class UserService : UserManager<User, Guid>, IUserService
    {
        #region Private Fields

        private readonly ILocationService _addressService;
        private readonly ILocationCityService _cityService;
        private readonly ICommonService _commonService;
        private readonly ICompanyService _companyService;
        private readonly IConfigurationManager _configurationManager;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IRoleService _roleService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<UserMeta> _userMetaRepository;
        private readonly IDbSet<User> _userRepository;
        private readonly IDbSet<UserRole> _userRoleRepository;
        private readonly IWebContextManager _webContextManager;
        private readonly IModelValidator _modelValidator;

        #endregion Private Fields

        #region Public Constructors

        public UserService(IMapper mapper, IUserStore<User, Guid> userStore, IDataProtectionProvider dataProtectionProvider, IIdentityMessageService smsService, IIdentityMessageService emailService, ICompanyService companyService, IProductService productService, ILocationService addressService, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IRoleService roleService, IConfigurationManager configurationManager, ICommonService commonService, IEventPublisher eventPublisher, ILocationCityService cityService, IModelValidator modelValidator)
            : base(userStore)
        {
            _mapper = mapper;
            _dataProtectionProvider = dataProtectionProvider;
            _companyService = companyService;
            _productService = productService;
            _addressService = addressService;
            _userRepository = unitOfWork.Set<User>();
            _userMetaRepository = unitOfWork.Set<UserMeta>();
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _roleService = roleService;
            _configurationManager = configurationManager;
            _commonService = commonService;
            _eventPublisher = eventPublisher;
            _cityService = cityService;
            _modelValidator = modelValidator;
            _userRoleRepository = unitOfWork.Set<UserRole>();
            SmsService = smsService;
            EmailService = emailService;
            AutoCommitEnabled = true;
            UserManagerOptions();
        }

        #endregion Public Constructors

        #region Public Properties

        public bool AutoCommitEnabled { get; set; }
        
        public Guid CurrentRoleId => _userRepository.AsNoTracking().Where(m => m.Id == _webContextManager.CurrentUserId).Select(m => m.Roles.Select(r => r.RoleId).FirstOrDefault()).SingleOrDefault();

        #endregion Public Properties

        #region Public Methods

        public async Task AddToRoleByIdAsync(Guid userId, UserRole userRole)
        {
            if (userRole == null)
                throw new ArgumentNullException(nameof(userRole));

            var user = await _userRepository.FirstOrDefaultAsync(model => model.Id == userId);
            user?.Roles.Clear();
            user?.Roles.Add(userRole);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task<int> CountAllAsync()
        {
            var request = new UserSearchModel();
          return  await CountByRequestAsync(request);
            
        }

        public async Task<int> CountByRequestAsync(UserSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(UserCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var user = _mapper.Map<User>(model);
            await CreateAsync(user, model.Password);

            _eventPublisher.EntityInserted(user);
        }

        public async Task CreateUserMetaByUserIdAsync(Guid userId)
        {
            var userMeta = new UserMeta
            {
                IsActive = true,
                CreatedById = userId,
                Location = new Location()
            };
            _userMetaRepository.Add(userMeta);
        }

        public async Task CreateUserMetaByViewModelAsync(UserCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var userMeta = _mapper.Map<UserMeta>(model);
            var defaultLocation = await _addressService.GetDefaultLocationAsync();
            userMeta.IsActive = true;
            //userMeta.CreatedById = _webContextManager.CurrentUserId;
            userMeta.Location = new Location()
            {
                Latitude = defaultLocation.Item1,
                Longitude = defaultLocation.Item2,
                CityId = await _cityService.GetIdByNameAsync(defaultLocation.Item3)
            };
            _userMetaRepository.Add(userMeta);

            await _unitOfWork.SaveAllChangesAsync(auditUserId: model.CreatedById);

            _eventPublisher.EntityInserted(userMeta);
        }

        public async Task DeleteByIdAsync(Guid userId)
        {
            var user = await FindByUserIdAsync(userId);
            await _productService.DeleteByUserIdAsync(userId);
            await _companyService.DeleteByUserIdAsync(userId);
            await _addressService.DeleteByIdAsync(user.Meta.LocationId.GetValueOrDefault());
            user.CompanyFollows.Clear();
            user.CompanyQuestions.Clear();
            user.ProductComments.Clear();
            user.ProductLikes.Clear();
            user.ProductsCommentLikes.Clear();
            user.Carts.Clear();
            user.Products.Clear();
            user.Receipts.Clear();
            _userRepository.Remove(user);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(user);
        }

        public async Task EditByViewModelAsync(UserEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var originalUser = await FindByUserIdAsync(model.Id);
            _mapper.Map(model, originalUser);
            originalUser.Meta.AvatarFileName = model.AvatarFileName;
            originalUser.Meta.FirstName = model.FirstName;
            originalUser.Meta.LastName = model.LastName;
            originalUser.Meta.HomeNumber = model.HomeNumber;
            originalUser.Meta.NationalCode = model.NationalCode;
            originalUser.Meta.Location.Latitude = model.Location.Latitude;
            originalUser.Meta.Location.Longitude = model.Location.Longitude;
            originalUser.Meta.Location.Extra = model.Location.Extra;
            originalUser.Meta.Location.PostalCode = model.Location.PostalCode;
            originalUser.Meta.Location.Street = model.Location.Street;
            originalUser.Meta.Location.CityId = model.Location.LocationCity.Id;
            originalUser.Meta.Gender = model.Gender;

            var role = new UserRole
            {
                RoleId = model.RoleId
            };
            originalUser.Roles.Clear();
            originalUser.Roles.Add(role);

            await _unitOfWork.SaveAllChangesAsync();
            this.UpdateSecurityStamp(model.Id);

            _eventPublisher.EntityUpdated(originalUser);
        }

        public async Task EditMetaByViewModelAsync(UserEditMeModel model, bool isCurrentUser = false)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

          var originalUser = await _userRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            if (isCurrentUser && originalUser.CreatedById == _webContextManager.CurrentUserId)
                return;

            _mapper.Map(model, originalUser);
            originalUser.Meta.AvatarFileName = model.AvatarFileName;
            originalUser.Meta.FirstName = model.FirstName;
            originalUser.Meta.LastName = model.LastName;
            originalUser.Meta.HomeNumber = model.HomeNumber;
            originalUser.Meta.NationalCode = model.NationalCode;
            originalUser.Meta.Location.Latitude = model.Location.Latitude;
            originalUser.Meta.Location.Longitude = model.Location.Longitude;
            originalUser.Meta.Location.Extra = model.Location.Extra;
            originalUser.Meta.Location.PostalCode = model.Location.PostalCode;
            originalUser.Meta.Location.Street = model.Location.Street;
            originalUser.Meta.Location.CityId = model.Location.LocationCity.Id;
            originalUser.Meta.Gender = model.Gender;

            await _unitOfWork.SaveAllChangesAsync();
        }

        public Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _userRepository.AsNoTracking().SingleOrDefaultAsync(model => model.Email == email, cancellationToken);
        }

        public async Task<User> FindByPhoneNumberAsync(string phoneNumber)
        {
            return await _userRepository.AsNoTracking().FirstOrDefaultAsync(model => model.PhoneNumber == phoneNumber);
        }

        public async Task<User> FindByUserIdAsync(Guid userId, bool isCurrentUser = false)
        {
            var query = _userRepository.AsQueryable()
                .Include(model => model.Meta);
            query = isCurrentUser ? query.Where(model => model.Id == _webContextManager.CurrentUserId) : query.Where(model => model.Id == userId);
            var user = await query.FirstOrDefaultAsync();

            return user;
        }

        public async Task<User> FindByUserNameAsync(string username)
        {
            return await _userRepository.FirstOrDefaultAsync(model => model.UserName == username);
        }

        public async Task<UserMeta> FindUserMetaByCurrentUserAsync()
        {
            return await _userMetaRepository
                .FirstOrDefaultAsync(model => model.CreatedById == _webContextManager.CurrentUserId);
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserService service, User user)
        {
            return await service.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public async Task<string> GenerateUserNameAsync()
        {
            var randomNumber = _commonService.RandomNumberByCount(100000000, 999999999);
            var generatedUserName = CodeConst.MyApp + randomNumber;
            if (await _userRepository.AsNoTracking().AnyAsync(model => model.UserName == generatedUserName))
                return await GenerateUserNameAsync();
            return generatedUserName;
        }

        public async Task<Location> GetAddressByIdAsync(Guid userId)
        {
            return await _userRepository
                .Include(model => model.Meta.Location)
                .Where(model => model.Id == userId)
                .Select(model => model.Meta.Location)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmailAndPasswordAsync(string email, string password, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));

            if (password == null)
                throw new ArgumentNullException(nameof(password));

            var user = await FindByEmailAsync(email);
            var result = await CheckPasswordAsync(user, password);

            return result ? user : null;
        }

        public async Task<bool> IsExistByEmailAndPasswordAsync(string email, string password, CancellationToken cancellationToken = default(CancellationToken))
        {
            _modelValidator.Validate<ObjectValidator, object>(email);
            _modelValidator.Validate<ObjectValidator, object>(password);

            var user = await FindByEmailAsync(email);
            var result = await CheckPasswordAsync(user, password);

            return result;
        }
        
        public async Task<bool> IsExistByPhoneNumberAsync(string phoneNumber)
        {
            _modelValidator.Validate<ObjectValidator, object>(phoneNumber);

            return await _userRepository.AsNoTracking().AnyAsync(user => user.PhoneNumber == phoneNumber);
        }

        public async Task<User> GetCurrentUserAsync()
        {
            return await _userRepository.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == _webContextManager.CurrentUserId);
        }

        public async Task<string> GetCurrentUserNameAsync()
        {
            var userMeta = await _userMetaRepository.AsNoTracking()
                .Include(model => model.CreatedBy)
                .FirstOrDefaultAsync(model => model.CreatedById == _webContextManager.CurrentUserId);

            if (userMeta.DisplayName != null)
                return userMeta.DisplayName;
            if (userMeta.FullName != " ")
                return userMeta.FullName;
            return userMeta.CreatedBy.UserName ?? userMeta.CreatedBy.Email;
        }

        public IUserEmailStore<User, Guid> GetEmailStore()
        {
            var cast = Store as IUserEmailStore<User, Guid>;
            if (cast == null)
                throw new NotSupportedException();

            return cast;
        }

        public async Task<IList<FineUploaderObject>> GetFileAsFineUploaderModelByIdAsync(Guid userId)
        {
            var s =
                (await _userRepository.AsNoTracking()
                .Include(model => model.Meta)
                    .Where(model => model.Id == userId)
                    .Select(model => model.Meta)
                    .ToListAsync())
                .Select(model => new FineUploaderObject
                {
                    id = model.Id.ToString(),
                    uuid = model.Id.ToString(),
                    name = model.AvatarFileName,
                    thumbnailUrl = FileConst.ImagesWebPath.PlusWord(model.AvatarFileName),
                    size = new FileInfo(HttpContext.Current.Server.MapPath(FileConst.ImagesWebPath.PlusWord(model.AvatarFileName))).Length.ToString()
                }).ToList();
            return s;
        }

        public async Task<IList<string>> GetPhoneNumbersByUserIdsAsync(IList<Guid?> userIds)
        {
            return await _userRepository.AsNoTracking()
                .Where(model => userIds.Contains(model.Id))
                .Select(model => model.PhoneNumber)
                .ToListAsync();
        }

        public override async Task<IList<string>> GetRolesAsync(Guid userId)
        {
            return await _roleService.GetPermissionNamesByUserIdAsync(userId);
        }

        public async Task<User> GetSystemUserAsync()
        {
            return await _userRepository.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IsSystemAccount == true);
        }

        public async Task<UserMeta> GetUserMetaByIdAsync(Guid userId)
        {
            return await _userMetaRepository.AsNoTracking()
                .FirstOrDefaultAsync(model => model.CreatedById == userId);
        }

        public async Task<IList<User>> GetUsersByRequestAsync(UserSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            //request.GroupBy = user => user.Code;

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<IList<User>> GetUsersByRoleIdAsync(Guid roleId)
        {
            var userIds = await _userRoleRepository.AsNoTracking()
                .Where(model => model.RoleId == roleId)
                .Select(model => model.UserId)
                .ToListAsync();

            return await _userRepository.AsNoTracking()
                .Where(model => userIds.Contains(model.Id))
                .ToListAsync();
        }

        public async Task<bool> HasUserNameByCurrentUserAsync()
        {
            var novinak = "Novinak";
           return  !await _userRepository.AsNoTracking()
                .Where(model => model.Id == _webContextManager.CurrentUserId && model.UserName.Contains(novinak))
                .Select(model => model.UserName)
                .AnyAsync();
        }

        public async Task<bool> IsBanByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _userRepository.AsNoTracking()
                .AnyAsync(user => user.Id == userId && user.IsBan == true);
        }

        public async Task<bool> IsBanByUserNameAsync(string userName)
        {
            return await _userRepository.AsNoTracking()
                .AnyAsync(user => user.UserName == userName.ToLower() && user.IsBan == true);
        }

        public async Task<bool> IsEmailConfirmedByEmailAsync(string email, HttpContext httpContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            var user = await _userRepository.AsNoTracking().SingleOrDefaultAsync(model => model.Email == email.ToLower());
            if (user == null)
                return true;

            if (!user.EmailConfirmed)
            {
                var code = await GenerateEmailConfirmationTokenAsync(user.Id);
                var url = httpContext.Request.Url;
                var callbackUrl =
                    $"{url.Scheme}://{url.Authority}/{_configurationManager.ConfirmationEmailUrl}?id={user.Id}&code={code.Base64ForUrlEncode()}";
                var subject = "تایید حساب کاربری";
                var body = "<span>" +
                           "لطفا جهت تایید حساب کاربری خود" +
                           $" <a href='{callbackUrl}'>اینجا کلیک کنید</a>" +
                           "</span>" +
                           "<hr/>" +
                           "<br/>".Repeat(2) +
                           "<span> در صورتی که لینک بالا کار نکرد ، لینک زیر را به صورت دستی در مرورگر خود وارد کنید</span>" +
                           "<br/>".Repeat(2) +
                           $"<span>{callbackUrl}</span>";

                await SendEmailAsync(user.Id, subject, body);
            }
            return user.EmailConfirmed;
        }

        public async Task<bool> IsExistByEmailAsync(string email, Guid? userId = null )
        {
            var query = _userRepository.AsQueryable();
            query = query.Where(user => user.Email == email.ToLower());

            if (userId.HasValue)
                query = query.Where(user => user.Id == userId);
            return await query.AnyAsync();
        }

        public async Task<bool> IsExistByEmailCancellationTokenAsync(string email,  CancellationToken cancellationToken = default(CancellationToken))
        {
          var dd = await _userRepository.AsNoTracking().AnyAsync(user => user.Email == email.ToLower());
            return dd;
        }

        public async Task<bool> IsExistByIdCancellationTokenAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _userRepository.AsNoTracking().AnyAsync(user => user.Id == id, cancellationToken);
        }

        public async Task<int> IsExistByUserNameAsync(string userName, Guid? userId= null, Guid? exceptUserId = null)
        {
            var query = _userRepository.AsQueryable();
            query = query.Where(user => user.UserName == userName);
            if (userId.HasValue)
                query = query.Where(user => user.Id == userId);
            if (exceptUserId.HasValue)
                query = query.Where(user => user.Id != exceptUserId);
            var list =  await query.ToListAsync();
            return list.Count();
        }

        public async Task<bool> IsExistByUserNameCancellationTokenAsync(string userName, CancellationToken cancellationToken)
        {
           var originUser =  await _userRepository.AsNoTracking().SingleOrDefaultAsync(user => user.Id == _webContextManager.CurrentUserId, cancellationToken);
            if (userName.StartsWith(CodeConst.MyApp) || userName == originUser.UserName)
                return true;

            var result = await _userRepository.AsNoTracking().AnyAsync(user => user.UserName == userName, cancellationToken);
            return !result;
        }

        public async Task<bool> IsLockedOutAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.AsNoTracking().SingleOrDefaultAsync(model => model.Id == userId);
            if (user == null)
                return true;
            return user.LockoutEnabled;
        }

        public async Task<bool> IsLockedOutAsync(string email, CancellationToken cancellationToken)
        {
            var user =await _userRepository.AsNoTracking().SingleOrDefaultAsync(model => model.Email == email);
            if (user == null)
                return true;
            return user.LockoutEnabled;
        }

        public async Task<bool> IsBanByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userRepository.AsNoTracking().SingleOrDefaultAsync(model => model.Email == email);
            if (user == null)
                return true;
            return user.IsBan != null && !user.IsBan.Value;
        }

        public async Task<string> MaxByRequestAsync(UserSearchModel model, string aggregateMember)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var users = QueryByRequest(model);
            switch (aggregateMember)
            {
                case "Code":
                    var memberMax = await users.MaxAsync(m => m.Code);
                    return memberMax;
            }

            return null;
        }

        public Func<CookieValidateIdentityContext, Task> OnValidateIdentity()
        {
            return UserSecurityStampValidator.OnValidateIdentity(TimeSpan.FromMinutes(0), GenerateUserIdentityAsync, identity => Guid.Parse(identity.GetUserId()));
        }

        public IQueryable<User> QueryByRequest(UserSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var users = _userRepository.AsNoTracking().AsQueryable()
                .Include(m => m.Meta);

            if (model.IsActive.HasValue)
                users = users.Where(m => m.IsActive == model.IsActive);
            if (model.IsBan.HasValue)
                if (model.IsBan.Value.ToString() != "-1")
                    users = users.Where(m => m.IsBan == model.IsBan);
            if (model.IsVerify.HasValue)
                if (model.IsVerify.Value.ToString() != "-1")
                    users = users.Where(m => m.IsVerify == model.IsVerify);
            if (model.Term.HasValue())
                users = users.Where(m => m.Email.Contains(model.Term) || m.UserName.Contains(model.Term) || m.Email.Contains(model.Term) || m.PhoneNumber.Contains(model.Term));

            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;

            //if(request.GroupBy != null)
            //    users = users.GroupBy(request.GroupBy).SelectMany(grouping => grouping);

            users = users.OrderBy($"{model.SortMember} {model.SortDirection}");

            return users;
        }

        public async void SeedDatabase()
        {
            var adminUser = _userRepository.FirstOrDefault(user => user.IsSystemAccount == true);
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = _configurationManager.AdminUserName,
                    IsSystemAccount = true,
                    Email = _configurationManager.AdminEmail
                };
                adminUser = new User
                {
                    IsActive = true,
                    CreatedById = adminUser.Id
                };
                this.Create(adminUser, _configurationManager.AdminPassword);
                this.SetLockoutEnabled(adminUser.Id, false);
            }
            var userRoles = await _roleService.GetRoleNamesByUserIdAsync(adminUser.Id);
            if (userRoles.Any(role => role == "مدیران"))
                return;
            this.AddToRole(adminUser.Id, "مدیران");
        }

        public void UserManagerOptions()
        {
            ClaimsIdentityFactory = new UserClaimsIdentityFactory();
            UserValidator = new UserValidator<User, Guid>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };
            PasswordValidator = new UserPasswordValidator
            {
                RequiredLength = 5,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;
            if (_dataProtectionProvider == null) return;
            var dataProtector = _dataProtectionProvider.Create("Application Identity");
            UserTokenProvider = new DataProtectorTokenProvider<User, Guid>(dataProtector);
        }
        
        public async Task<User> FindByPhoneNumberAsync(string phoneNumber, string password)
        {
            var user = _userRepository.AsNoTracking().First(usr => usr.PhoneNumber == phoneNumber);
            var verify = PasswordHasher.VerifyHashedPassword(user.PasswordHash, password);
            return verify == PasswordVerificationResult.Success ? user : null;
        }

        #endregion Public Methods
    }
}