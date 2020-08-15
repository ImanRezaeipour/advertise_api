using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Users;
using Advertise.Service.Common;
using Advertise.Service.Users;
using AutoMapper;

namespace Advertise.Service.Factory.Users
{
    public class UserBudgetFactory : IUserBudgetFactory
    {
        #region Private Fields

        private readonly IListService _listService;
        private readonly IUserBudgetService _userBudgetService;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public UserBudgetFactory(IUserBudgetService userBudgetService, IMapper mapper, IListService listService)
        {
            _userBudgetService = userBudgetService;
            _mapper = mapper;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<UserBudgetListModel> PrepareListModelAsync(bool isCurrentUser = false, Guid? userId = null)
        {
            var request = new UserBudgetSearchModel();
            request.TotalCount = await _userBudgetService.CountByRequestAsync(request);
            var userBudget = await _userBudgetService.GetByRequestAsync(request);
            var userBudgetViewModel = _mapper.Map<IList<UserBudgetModel>>(userBudget);
            var viewModel = new UserBudgetListModel
            {
                SearchModel = request,
                UserBudgets = userBudgetViewModel,
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                PageSizeList = await _listService.GetPageSizeListAsync()
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}