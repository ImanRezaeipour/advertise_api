using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Users;
using Advertise.Service.Common;
using Advertise.Service.Users;
using AutoMapper;

namespace Advertise.Service.Factory.Users
{
    public class UserViolationFactory : IUserViolationFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IUserViolationService _userViolationService;

        #endregion Private Fields

        #region Public Constructors

        public UserViolationFactory(ICommonService commonService, IMapper mapper, IUserViolationService userViolationService, IListService listService)
        {
            _commonService = commonService;
            _mapper = mapper;
            _userViolationService = userViolationService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<UserViolationDetailModel> PrepareDetailModelAsync(Guid userReportId)
        {
            var userReport = await _userViolationService.FindByIdAsync(userReportId);
            var viewModel = _mapper.Map<UserViolationDetailModel>(userReport);

            return viewModel;
        }

        public async Task<UserViolationEditModel> PrepareEditModelAsync(Guid userReportId)
        {
            var userReport = await _userViolationService.FindByIdAsync(userReportId);
            var viewModel = _mapper.Map<UserViolationEditModel>(userReport);

            return viewModel;
        }

        public async Task<UserViolationListModel> PrepareListModelAsync(UserViolationSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _userViolationService.CountByRequestAsync(model);
            var userReport = await _userViolationService.GetByRequestAsync(model);
            var userReportViewModel = _mapper.Map<IList<UserViolationModel>>(userReport);
            var viewModel = new UserViolationListModel
            {
                SearchModel = model,
                UserViolations = userReportViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync()
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}