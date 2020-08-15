using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Helpers;
using Advertise.Core.Model.Plans;
using Advertise.Core.Types;
using Advertise.Service.Common;
using Advertise.Service.Plans;
using Advertise.Service.Roles;
using AutoMapper;

namespace Advertise.Service.Factory.Plans
{
    public class PlanFactory : IPlanFactory
    {
        #region Private Fields

        private readonly CommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IPlanService _planService;
        private readonly IRoleService _roleService;

        #endregion Private Fields

        #region Public Constructors

        public PlanFactory(IPlanService planService, CommonService commonService, IMapper mapper, IRoleService roleService, IListService listService)
        {
            _planService = planService;
            _commonService = commonService;
            _mapper = mapper;
            _roleService = roleService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<PlanCreateModel> PrepareCreateModelAsync(PlanCreateModel modelPrepare = null)
        {
            var viewModel = modelPrepare?? new PlanCreateModel();
            viewModel.RoleList = await _roleService.GetRolesAsSelectListAsync();
            viewModel.PlanDurationList = EnumHelper.CastToSelectListItems<PlanDurationType>();
            viewModel.ColorTypeList = EnumHelper.CastToSelectListItems<ColorType>();

            return viewModel;
        }

        public async Task<PlanEditModel> PrepareEditModelAsync(Guid id, PlanEditModel modelPrepare= null)
        {
            var plan = await _planService.FindByIdAsync(id);
            var viewModel = modelPrepare?? _mapper.Map<PlanEditModel>(plan);
            viewModel.RoleList = await _roleService.GetRolesAsSelectListAsync();
            viewModel.PlanDurationList = EnumHelper.CastToSelectListItems<PlanDurationType>();
            viewModel.ColorTypeList = EnumHelper.CastToSelectListItems<ColorType>(); 

            return viewModel;
        }

        public async Task<PlanListModel> PrepareListModelAsync(PlanSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.UserId = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _planService.CountByRequestAsync(model);
            var list = await _planService.GetByRequestAsync(model);
            var plans = _mapper.Map<IList<PlanModel>>(list);
            var planListViewModel = new PlanListModel
            {
                Plans = plans,
                SearchModel = model,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
            };

            return planListViewModel;
        }

        #endregion Public Methods
    }
}