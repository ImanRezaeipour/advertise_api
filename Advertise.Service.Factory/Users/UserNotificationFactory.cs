using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Users;
using Advertise.Service.Common;
using Advertise.Service.Users;
using AutoMapper;

namespace Advertise.Service.Factory.Users
{
    public class UserNotificationFactory : IUserNotificationFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IUserNotificationService _notificationService;

        #endregion Private Fields

        #region Public Constructors

        public UserNotificationFactory(ICommonService commonService, IUserNotificationService notificationService, IMapper mapper, IListService listService)
        {
            _commonService = commonService;
            _notificationService = notificationService;
            _mapper = mapper;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<UserNotificationListModel> PrepareListModelAsync(UserNotificationSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _notificationService.CountByRequestAsync(model);
            var notifications = await _notificationService.GetByRequestAsync(model);
            var notificationViewModel = _mapper.Map<IList<UserNotificationModel>>(notifications);
            var viewModel = new UserNotificationListModel
            {
                SearchModel = model,
                UserNotifications = notificationViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
            };

            if (isCurrentUser)
                viewModel.IsMine = true;

            return viewModel;
        }

        #endregion Public Methods
    }
}

