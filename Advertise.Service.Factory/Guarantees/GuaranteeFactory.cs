using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Exceptions;
using Advertise.Core.Model.Guarantees;
using Advertise.Service.Common;
using Advertise.Service.Guarantees;
using AutoMapper;

namespace Advertise.Service.Factory.Guarantees
{
    public class GuaranteeFactory : IGuaranteeFactory
    {
        private readonly IGuaranteeService _guaranteeService;
        private readonly IMapper _mapper;
        private readonly IListService _listService;
        private readonly ICommonService _commonService;

        public GuaranteeFactory(IGuaranteeService guaranteeService, IMapper mapper, ICommonService commonService, IListService listService)
        {
            _guaranteeService = guaranteeService;
            _mapper = mapper;
            _commonService = commonService;
            _listService = listService;
        }

        public async Task<GuaranteeEditModel> PrepareEditModelAsync(Guid id, GuaranteeEditModel modelPrepare = null)
        {
            var manufaturer = await _guaranteeService.FindByIdAsync(id);

            if (manufaturer == null)
                throw new FactoryException();

            var viewModel = modelPrepare ?? _mapper.Map<GuaranteeEditModel>(manufaturer);

            return viewModel;
        }

        public async Task<GuaranteeListModel> PrepareListModelAsync(GuaranteeSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            var guarantees = await _guaranteeService.GetByRequestAsync(model);
            var guaranteeViewModels = _mapper.Map<List<GuaranteeModel>>(guarantees);
            model.TotalCount = await _guaranteeService.CountByRequestAsync(model);

            var guaranteeListViewModel = new GuaranteeListModel
            {
                SearchModel = model,
                Guarantees = guaranteeViewModels,
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                PageSizeList = await _listService.GetPageSizeListAsync()
            };

            return guaranteeListViewModel;
        }
    }
}