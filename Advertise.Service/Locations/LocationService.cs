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
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Locations;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Locations
{
    public class LocationService : ILocationService
    {
        #region Private Fields

        private readonly IDbSet<Location> _addressRepository;
        private readonly ILocationCityService _cityService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public LocationService(IMapper mapper, IUnitOfWork unitOfWork, ILocationCityService cityService, IEventPublisher eventPublisher, IWebContextManager webContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cityService = cityService;
            _eventPublisher = eventPublisher;
            _webContextManager = webContextManager;
            _addressRepository = unitOfWork.Set<Location>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(LocationSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(LocationCreateModel model)
        {
            if (model == null)
                throw new ArgumentException(nameof(model));

            var address = _mapper.Map<Location>(model);
            address.CreatedById = _webContextManager.CurrentUserId;
            _addressRepository.Add(address);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(address);
        }

        public async Task DeleteByIdAsync(Guid addressId)
        {
            var address = await _addressRepository.FirstOrDefaultAsync(model => model.Id == addressId);
            _addressRepository.Remove(address);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(address);
        }

        public async Task EditByViewModelAsync(LocationEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var orginalAddress = await _addressRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, orginalAddress);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(orginalAddress);
        }

        public async Task<Location> FindByIdAsync(Guid addressId)
        {
            return  await _addressRepository.FirstOrDefaultAsync(model => model.Id == addressId);
        }

        public async Task<Location> FindDefaultAsync()
        {
            var address = await _addressRepository
                .AsNoTracking()
                .FirstOrDefaultAsync(model => model.Latitude == "0");

            return address;
        }

        public async Task<Tuple<string, string,string>> GetDefaultLocationAsync()
        {
            return Tuple.Create("35.67751121777174", "51.394007801427506","تهران");
        }

        public async Task<IList<Location>> GetByRequestAsync(LocationSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

           return  await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<IList<SelectList>> GetProvinceAsSelectListItemAsync()
        {
            var request = new LocationCitySearchModel
            {
                SortDirection = SortDirection.Asc,
                SortMember = SortMember.Name,
                PageSize = PageSize.Count100,
                IsState = true,
                IsActive = true
            };
            var cityList = await _cityService.GetByRequestAsync(request);
            return cityList.Select(city => new SelectList
            {
                Text = city.Name,
                Value = city.Id.ToString()
            }).ToList();
        }

        public IQueryable<Location> QueryByRequest(LocationSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var addresses = _addressRepository.AsNoTracking().AsQueryable();
            if (model.UserId.HasValue)
                addresses = addresses.Where(address => address.CreatedById == model.UserId);
            return  addresses.OrderBy($"{model.SortMember} {model.SortDirection}");
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}