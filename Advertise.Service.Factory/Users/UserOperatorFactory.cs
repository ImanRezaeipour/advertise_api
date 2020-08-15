using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Users;
using Advertise.Service.Common;
using Advertise.Service.Users;
using AutoMapper;

namespace Advertise.Service.Factory.Users
{
    public class UserOperatorFactory : IUserOperatorFactory
    {
        private readonly IUserOperationServive _userOperationService;
        private readonly ICommonService _commonService;
        private readonly IMapper _mapper;
        private readonly IListService _listService;

        public UserOperatorFactory(IUserOperationServive userOperationService, ICommonService commonService,  IMapper mapper, IListService listService)
        {
            _userOperationService = userOperationService;
            _commonService = commonService;
            _mapper = mapper;
            _listService = listService;
        }

        public async Task<UserOperatorListModel> PrepareListModel(UserOperatorSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _userOperationService.CountByRequest(model);
            var user = await _userOperationService.GetByRequestAsync(model);
            var userViewModel = _mapper.Map<IList<UserOperatorModel>>(user);
            var viewModel = new UserOperatorListModel
            {
                SearchModel = model,
                UserOperators = userViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync()
            };

            return viewModel;
        }
    }
}
