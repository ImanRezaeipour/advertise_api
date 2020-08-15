using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Exceptions;
using Advertise.Core.Helpers;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Carts;
using Advertise.Core.Model.General;
using Advertise.Core.Model.Locations;
using Advertise.Core.Model.Users;
using Advertise.Core.Types;
using Advertise.Service.Carts;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using Advertise.Service.Locations;
using Advertise.Service.Plans;
using Advertise.Service.Roles;
using Advertise.Service.Users;
using AutoMapper;

namespace Advertise.Service.Factory.Users
{
    public class UserFactory : IUserFactory
    {
        #region Private Fields

        private readonly ILocationService _addressService;
        private readonly ICartService _bagService;
        private readonly ICommonService _commonService;
        private readonly ICompanyService _companyService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        private readonly IPlanService _planService;
        private readonly IUserService _userService;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public UserFactory(ICartService bagService, IMapper mapper, IRoleService roleService, ILocationService addressService, ICompanyService companyService, IWebContextManager webContextManager, ICommonService commonService, IUserService userService, IPlanService planService, IListService listService)
        {
            _bagService = bagService;
            _mapper = mapper;
            _roleService = roleService;
            _addressService = addressService;
            _companyService = companyService;
            _webContextManager = webContextManager;
            _commonService = commonService;
            _userService = userService;
            _planService = planService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods


        public async Task<DashboardHeaderModel> PrepareDashboardHeaderModelAsync()
        {
            var userMeta = await _userService.FindUserMetaByCurrentUserAsync();
            var viewModel = _mapper.Map<DashboardHeaderModel>(userMeta);

            return viewModel;
        }

        public async Task<UserDetailModel> PrepareDetailModelAsync(string userName)
        {
            var userNovinak = userName.StartsWith(CodeConst.MyApp);
            if (userNovinak)
                throw new ValidationException(" وجود کلمه نویناک");

            var user = await _userService.FindByUserNameAsync(userName);
            var userMeta = await _userService.GetUserMetaByIdAsync(user.Id);
            var viewModel = _mapper.Map<UserDetailModel>(userMeta);
            var company = await _companyService.FindByUserIdAsync(viewModel.CreatedById);
            viewModel.CompanyTitle = company.Title;
            viewModel.CompanyAlias = company.Alias;
            viewModel.CompanyId = company.Id;
            viewModel.UserName = user.UserName;

            return viewModel;
        }

        public async Task<UserEditModel> PrepareEditModelAsync(string userName = null, bool isCurrentUser = false, UserEditModel modelPrepare = null)
        {
            var user = isCurrentUser ? await _userService.GetCurrentUserAsync() : await _userService.FindByUserNameAsync(userName);
            var viewModel = modelPrepare ?? _mapper.Map<UserEditModel>(user);

            var userMeta = await _userService.GetUserMetaByIdAsync(viewModel.Id);
            if(userMeta == null)
                throw new FactoryException("اطلاعات تکمیلی موجود نمی باشد");

            viewModel.FirstName = userMeta.FirstName;
            viewModel.LastName = userMeta.LastName;
            viewModel.AvatarFileName = userMeta.AvatarFileName;
            viewModel.Gender = userMeta.Gender.GetValueOrDefault();
            viewModel.NationalCode = userMeta.NationalCode;
            viewModel.HomeNumber = userMeta.HomeNumber;
            viewModel.RoleList = await _roleService.GetRolesAsSelectListAsync();
            viewModel.GenderList = EnumHelper.CastToSelectListItems<GenderType>();
            viewModel.AddressProvince = await _addressService.GetProvinceAsSelectListItemAsync();
            viewModel.IsSetUserName = !viewModel.UserName.StartsWith(CodeConst.MyApp);

            var role = await _roleService.FindByUserIdAsync(viewModel.Id);
            if (role != null)
                viewModel.RoleId = role.Id;

            var address = await _addressService.FindByIdAsync(userMeta.LocationId.GetValueOrDefault());
            if (address != null)
            {
                viewModel.Location = _mapper.Map<LocationModel>(address);
                if (viewModel.Location.LocationCity == null)
                    viewModel.Location.LocationCity = new LocationCityModel();
            }
            else
            {
                viewModel.Location = new LocationModel
                {
                    LocationCity = new LocationCityModel()
                };
            }

            if (isCurrentUser)
                viewModel.IsMine = true;

            return viewModel;
        }

        public async Task<UserHeaderModel> PrepareHeaderModelAsync()
        {
            var userMeta = await _userService.GetUserMetaByIdAsync(_webContextManager.CurrentUserId);
            var user = await _userService.FindByIdAsync(_webContextManager.CurrentUserId);
            var viewModel = _mapper.Map<UserHeaderModel>(userMeta);

            viewModel.DisplayName = await _userService.GetCurrentUserNameAsync();
            viewModel.IsSetUserName = await _userService.HasUserNameByCurrentUserAsync();
            viewModel.IsSetSubdomain = await _companyService.HasAliasByCurrentUserAsync();
            viewModel.UserName = user.UserName;
            var bagRequest = new CartSearchModel
            {
                CreatedById = _webContextManager.CurrentUserId
            };
            viewModel.BagCount = await _bagService.CountByRequestAsync(bagRequest);

            return viewModel;
        }

        public async Task<UserListModel> PrepareListModelAsync(UserSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _userService.CountByRequestAsync(model);
            var user = await _userService.GetUsersByRequestAsync(model);
            var userViewModel = _mapper.Map<IList<UserModel>>(user);
            var viewModel = new UserListModel
            {
                SearchModel = model,
                Users = userViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                IsActiveList = EnumHelper.CastToSelectListItems<ActiveType>(),
                IsBanList = await _listService.GetIsBanListAsync(),
                IsVerifyList = await _listService.GetIsVerifyListAsync()
            };

            return viewModel;
        }

        public async Task<ProfileHeaderModel> PrepareProfileHeaderModelAsync()
        {
            var userMeta = await _userService.FindUserMetaByCurrentUserAsync();
            var viewModel = _mapper.Map<ProfileHeaderModel>(userMeta);

            return viewModel;
        }

        #endregion Public Methods
    }
}