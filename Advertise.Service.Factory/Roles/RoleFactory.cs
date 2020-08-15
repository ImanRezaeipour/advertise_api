using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Roles;
using Advertise.Service.Common;
using Advertise.Service.Roles;
using AutoMapper;

namespace Advertise.Service.Factory.Roles
{
    public class RoleFactory : IRoleFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        #endregion Private Fields

        #region Public Constructors

        public RoleFactory(ICommonService commonService, IMapper mapper, IRoleService roleService, IListService listService)
        {
            _commonService = commonService;
            _mapper = mapper;
            _roleService = roleService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<RoleEditModel> PrepareEditModelAsync(Guid roleId)
        {
            if (roleId == null)
                throw new ArgumentNullException(nameof(roleId));

            var role = await _roleService.FindAsync(roleId);
            var viewModel = _mapper.Map<RoleEditModel>(role);

            return viewModel;
        }

        public async Task<RoleListModel> PrepareListModelAsync(RoleSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _roleService.CountByRequestAsync(model);
            var role = await _roleService.GetRolesByRequestAsync(model);
            var roleViewModel = _mapper.Map<IList<RoleModel>>(role);
            var viewModel = new RoleListModel
            {
                SearchModel = model,
                Roles = roleViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}