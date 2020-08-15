using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Plans;
using Advertise.Service.Common;
using Advertise.Service.Plans;
using AutoMapper;

namespace Advertise.Service.Factory.Plans
{
    public class PlanPaymentFactory : IPlanPaymentFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IPlanService _planService;
        private readonly IPlanPaymentService _planPaymentService;

        #endregion Private Fields

        #region Public Constructors

        public PlanPaymentFactory(ICommonService commonService, IPlanPaymentService planPaymentService, IPlanService planService, IMapper mapper, IListService listService)
        {
            _commonService = commonService;
            _planPaymentService = planPaymentService;
            _planService = planService;
            _mapper = mapper;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<PlanPaymentListModel> PrepareListModel(PlanPaymentSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _planPaymentService.CountByRequestAsync(model);
            var planPayments = await _planPaymentService.GetByRequestAsync(model);
            var planPaymentViewModel = _mapper.Map<IList<PlanPaymentModel>>(planPayments);
            var viewModel = new PlanPaymentListModel
            {
                PlanPayments = planPaymentViewModel,
                SearchModel = model,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
            };

            if (isCurrentUser)
                viewModel.IsMine = true;

            return viewModel;
        }

        public async Task<PlanPyamentCreateModel> PrepareCreateModel()
        {
            var request = new PlanSearchModel();
            var plans = await _planService.GetByRequestAsync(request);
            var planViewModel = _mapper.Map<IList<PlanModel>>(plans);
            var viewModel = new PlanPyamentCreateModel
            {
                Plans = planViewModel
            };
            return viewModel;
        }

        #endregion Public Methods
    }
}