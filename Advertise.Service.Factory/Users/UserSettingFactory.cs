using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Domain.Users;
using Advertise.Core.Helpers;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Users;
using Advertise.Core.Types;
using Advertise.Service.Common;
using Advertise.Service.Users;
using AutoMapper;

namespace Advertise.Service.Factory.Users
{
    public class UserSettingFactory : IUserSettingFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IUserSettingService _userSettingService;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public UserSettingFactory(ICommonService commonService, IMapper mapper, IUserSettingService userSettingService, IWebContextManager webContextManager, IListService listService)
        {
            _commonService = commonService;
            _mapper = mapper;
            _userSettingService = userSettingService;
            _webContextManager = webContextManager;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<UserSettingEditModel> PrepareEditModelAsync(Guid? id = null, bool isCurrentUser = false, UserSettingEditModel modelPrepare = null)
        {
          var userSetting = (isCurrentUser ? await _userSettingService.FindByUserIdAsync(_webContextManager.CurrentUserId) : await _userSettingService.FindByUserIdAsync(id.Value)) ??
                            new UserSetting
                            {
                                Language = LanguageType.Persian,
                                Theme = ThemeType.White
                            };
            var viewModel = _mapper.Map<UserSettingEditModel>(userSetting);
            viewModel.LanguageList = EnumHelper.CastToSelectListItems<LanguageType>();
            viewModel.ThemeList = EnumHelper.CastToSelectListItems<ThemeType>();

            if (isCurrentUser)
                viewModel.IsMine = true;

            return viewModel;
        }
        
        public async Task<UserSettingListModel> PrepareListModelAsync(UserSettingSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _userSettingService.CountByRequestAsync(model);
            var userSetting = await _userSettingService.GetByRequestAsync(model);
            var userSettingViewModel = _mapper.Map<IList<UserSettingModel>>(userSetting);
            var viewModel = new UserSettingListModel
            {
                SearchModel = model,
                UserSettings = userSettingViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync()
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}