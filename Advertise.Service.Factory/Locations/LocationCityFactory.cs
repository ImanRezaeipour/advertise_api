using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Locations;
using Advertise.Service.Common;
using Advertise.Service.Locations;
using AutoMapper;

namespace Advertise.Service.Factory.Locations
{
    public class LocationCityFactory : ILocationCityFactory
    {
        #region Private Fields

        private readonly ILocationCityService _cityService;
        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public LocationCityFactory(ILocationCityService cityService, IMapper mapper, ICommonService commonService, IListService listService)
        {
            _cityService = cityService;
            _mapper = mapper;
            _commonService = commonService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<LocationCityListModel> PrepareListModelAsync(LocationCitySearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.UserId = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _cityService. CountByRequestAsync(model);
            var cityes = await _cityService.GetByRequestAsync(model);
            var cityesViewModel = _mapper.Map<IList<LocationCityModel>>(cityes);
            var viewModel = new LocationCityListModel
            {
                SearchModel = model,
                LocationCities = cityesViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync()
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}