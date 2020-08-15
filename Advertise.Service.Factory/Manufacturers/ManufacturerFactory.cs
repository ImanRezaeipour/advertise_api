using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Exceptions;
using Advertise.Core.Helpers;
using Advertise.Core.Model.Manufacturers;
using Advertise.Core.Types;
using Advertise.Service.Common;
using Advertise.Service.Manufacturers;
using AutoMapper;

namespace Advertise.Service.Factory.Manufacturers
{
    public class ManufacturerFactory : IManufacturerFactory
    {
        private readonly IManufacturerService _manufacturerService;
        private readonly IMapper _mapper;
        private readonly IListService _listService;
        private readonly ICommonService _commonService;

        public ManufacturerFactory(IManufacturerService manufacturerService, IMapper mapper, ICommonService commonService, IListService listService)
        {
            _manufacturerService = manufacturerService;
            _mapper = mapper;
            _commonService = commonService;
            _listService = listService;
        }

        public async Task<ManufacturerEditModel> PrepareEditModelAsync(Guid id, ManufacturerEditModel modelPrepare = null)
        {
            var manufaturer = await _manufacturerService.FindByIdAsync(id);

            if (manufaturer == null)
                throw new FactoryException();

            var viewModel = modelPrepare ?? _mapper.Map<ManufacturerEditModel>(manufaturer);
            viewModel.CountryList = EnumHelper.CastToSelectListItems<CountryType>();

            return viewModel;
        }

        public async Task<ManufacturerCreateModel> PrepareCreateModelAsync(ManufacturerCreateModel modelPrepare = null)
        {
            var viewModel = modelPrepare ?? new ManufacturerCreateModel();
            viewModel.CountryList = EnumHelper.CastToSelectListItems<CountryType>();

            return viewModel;
        }

        public async Task<ManufacturerListModel> PrepareListModelAsync(ManufacturerSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            var manufacturers = await _manufacturerService.GetByRequestAsync(model);
            var manufacturerViewModels = _mapper.Map<List<ManufacturerModel>>(manufacturers);
            model.TotalCount = await _manufacturerService.CountByRequestAsync(model);

            var manufacturerListViewModel = new ManufacturerListModel
            {
                SearchModel = model,
                Manufacturers = manufacturerViewModels,
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                PageSizeList = await _listService.GetPageSizeListAsync()
            };

            return manufacturerListViewModel;
        }
    }
}