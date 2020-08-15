using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Permissions;
using Advertise.Service.Common;
using Advertise.Service.Permissions;
using AutoMapper;

namespace Advertise.Service.Factory.Permissions
{
    public class PermissionFactory : IPermissionFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IPermissionService _permissionService;

        #endregion Private Fields

        #region Public Constructors

        public PermissionFactory(IMapper mapper, IPermissionService permissionService, ICommonService commonService, IListService listService)
        {
            _mapper = mapper;
            _permissionService = permissionService;
            _commonService = commonService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<PermissionEditModel> PrepareEditModelAsync(Guid permissionId)
        {
            var permission = await _permissionService.FindByIdAsync(permissionId);
            var viewModel = _mapper.Map<PermissionEditModel>(permission);

            return viewModel;
        }

        public async Task<PermissionListModel> PrepareListModel(PermissionSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _permissionService.CountByRequestAsync(model);
            var list = await _permissionService.GetByRequestAsync(model);
            var permissions = _mapper.Map<IList<PermissionModel>>(list);
            var permissionListViewModel = new PermissionListModel
            {
                Permissions = permissions,
                SearchModel = model,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
            };

            return permissionListViewModel;
        }

        #endregion Public Methods
    }
}