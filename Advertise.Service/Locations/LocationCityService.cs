using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Locations;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Locations;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Locations
{
    public class LocationCityService : ILocationCityService
    {
        #region Private Fields

        private readonly IDbSet<LocationCity> _cityRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public LocationCityService(IMapper mapper, IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _cityRepository = unitOfWork.Set<LocationCity>();
        }

        #endregion Public Constructors

        #region Public Methods

        public Task<int> CountByRequestAsync(LocationCitySearchModel model)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteByIdAsync(Guid cityId)
        {
            var city = await _cityRepository.AsNoTracking().FirstOrDefaultAsync(model => model.Id == cityId);
            _cityRepository.Remove(city);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityDeleted(city);
        }

        public async Task<LocationCityModel> GetLocationAsync(Guid cityId)
        {
            var city =await _cityRepository.FirstOrDefaultAsync(model => model.Id == cityId);
            return _mapper.Map<LocationCityModel>(city);
        }

        public async Task<LocationCity> FindByIdAsync(Guid id)
        {
           return await _cityRepository
                .FirstOrDefaultAsync(model => model.Id == id);
        }

        public async Task<Guid?> GetIdByNameAsync(string cityName)
        {
            return await _cityRepository.AsNoTracking()
                .Where(model => model.Name == cityName && model.IsState == false)
                .Select(model => model.Id)
                .SingleOrDefaultAsync();
        }

        public async Task<LocationCity> FindDefaultAsync()
        {
            return  await _cityRepository
                .AsNoTracking()
                .FirstOrDefaultAsync(model => model.Latitude == "0");
        }

        public async Task<IList<LocationCity>> GetByRequestAsync(LocationCitySearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return  await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<IList<SelectList>> GetCityAsSelectListItemAsync(Guid cityId)
        {
            var request = new LocationCitySearchModel
            {
                SortDirection = SortDirection.Asc,
                SortMember = SortMember.Name,
                PageSize = PageSize.Count100,
                ParentId = cityId
            };
            var cityList = await GetByRequestAsync(request);
           return  cityList.Select(city => new SelectList
            {
                Text = city.Name,
                Value = city.Id.ToString()
            }).ToList();
        }

        public async Task<object> GetStatesAsync()
        {
            var request = new LocationCitySearchModel
            {
                SortDirection = SortDirection.Asc,
                SortMember = SortMember.Name,
                PageSize = PageSize.Count100,
                IsActive = true,
                IsState = true
            };
            var states = await GetByRequestAsync(request);

            var viewModel = _mapper.Map<List<LocationCityModel>>(states);
           return  viewModel.Select(model => new
            {
                model.Name,
                model.ParentId,
                model.Longitude,
                model.Latitude
            }).ToList();
        }

        public async Task<string> GetNameByIdAsync(Guid cityId)
        {
            return (await _cityRepository.AsNoTracking().FirstOrDefaultAsync(model => model.Id == cityId)).Name;
        }

        public IQueryable<LocationCity> QueryByRequest(LocationCitySearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var cities = _cityRepository.AsNoTracking().AsQueryable();
           if (model.IsState.HasValue)
                cities = cities.Where(city => city.IsState == model.IsState);
            if (model.IsActive.HasValue)
                cities = cities.Where(city => city.IsActive == model.IsActive);
            if (model.ParentId.HasValue)
                cities = cities.Where(city => city.ParentId == model.ParentId);
            if (model.Term.HasValue())
                cities = cities.Where(city => city.Name == model.Term);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            cities = cities.OrderBy($"{model.SortMember} {model.SortDirection}");

            return cities;
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}