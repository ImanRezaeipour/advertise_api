using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Plans;
using Advertise.Service.Common;
using Advertise.Service.Plans;
using AutoMapper;

namespace Advertise.Service.Factory.Plans
{
    public class PlanDiscountFactory : IPlanDiscountFactory
    {
        #region Private Fields

        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IPlanDiscountService _planDiscountService;

        #endregion Private Fields

        #region Public Constructors

        public PlanDiscountFactory(IPlanDiscountService planDiscountService, IMapper mapper, IListService listService)
        {
            _planDiscountService = planDiscountService;
            _mapper = mapper;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<PlanDiscountEditModel> PrepareEditModelAsync(Guid? id)
        {
            var planDiscount = await _planDiscountService.FindByIdAsync(id.GetValueOrDefault());
            var viewModel = _mapper.Map<PlanDiscountEditModel>(planDiscount);
            return viewModel;
        }

        public async Task<PlanDiscountListModel> PrepareListModelAsync(PlanDiscountSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.UserId = userId;
            model.TotalCount = await _planDiscountService.CountByRequestAsync(model);
            var list = await _planDiscountService.GetByRequestAsync(model);
            var planDiscounts = _mapper.Map<IList<PlanDiscountModel>>(list);
            var planListViewModel = new PlanDiscountListModel
            {
                PlanDiscounts = planDiscounts,
                SearchModel = model,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync()
            };

            return planListViewModel;
        }

        #endregion Public Methods
    }
}