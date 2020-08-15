using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Model.Locations;
using Advertise.Service.Common;
using Advertise.Service.Locations;
using AutoMapper;

namespace Advertise.Service.Factory.Locations
{
    public class LocationFactory : ILocationFactory
    {
        #region Private Fields

        private readonly ILocationService _addressService;
        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public LocationFactory(ILocationService addressService, IMapper mapper, ICommonService commonService, IListService listService)
        {
            _addressService = addressService;
            _mapper = mapper;
            _commonService = commonService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<LocationEditModel> PrepareEditModelAsync(Guid addressId)
        {
            var address = await _addressService.FindByIdAsync(addressId);
            var viewModel = _mapper.Map<LocationEditModel>(address);

            return viewModel;
        }

        public async Task<LocationListModel> PrepareListModelAsync(LocationSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.SortDirection = SortDirection.Desc;
            model.UserId = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _addressService.CountByRequestAsync(model);
            var addresses = await _addressService.GetByRequestAsync(model);
            var addressesViewModel = _mapper.Map<IList<LocationModel>>(addresses);
            var viewModel = new LocationListModel
            {
                SearchModel = model,
                Locations = addressesViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync()
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}