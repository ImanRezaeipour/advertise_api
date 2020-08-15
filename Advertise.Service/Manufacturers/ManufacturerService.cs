using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Manufacturers;
using Advertise.Core.Exceptions;
using Advertise.Core.Extensions;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Manufacturers;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Manufacturers
{
    public class ManufacturerService : IManufacturerService
    {
        #region Private Fields

        private readonly IDbSet<Manufacturer> _manufacturerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public ManufacturerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _manufacturerRepository = unitOfWork.Set<Manufacturer>();
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<IList<SelectList>> GetAllAsSelectListAsync()
        {
            return await _manufacturerRepository.AsNoTracking()
                .Select(model => new SelectList
                {
                    Text = model.Name,
                    Value = model.Id.ToString()
                })
                .ToListAsync();
        }

        public async Task<Manufacturer> FindByIdAsync(Guid id)
        {
            return await _manufacturerRepository.SingleOrDefaultAsync(model => model.Id == id);
        }

        public async Task EditByViewMoodelAsync(ManufacturerEditModel model)
        {
            var manufaturer = await FindByIdAsync(model.Id);
            if(manufaturer == null)
                throw new ServiceException();

            _mapper.Map(model, manufaturer);
            await _unitOfWork.SaveAllChangesAsync();

        }

        public async Task CreateByViewModelAsync(ManufacturerCreateModel model)
        {
            var manufaturer = _mapper.Map<Manufacturer>(model);
            _manufacturerRepository.Add(manufaturer);
            await _unitOfWork.SaveAllChangesAsync();
        }

        public IQueryable<Manufacturer> QueryByRequest(ManufacturerSearchModel model)
        {
            var manufaturers = _manufacturerRepository.AsNoTracking().AsQueryable();

            if (model.Country.HasValue)
                manufaturers = manufaturers.Where(m => m.Country == model.Country);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            return manufaturers.OrderBy($"{model.SortMember} {model.SortDirection}");
        }

        public async Task<int> CountByRequestAsync(ManufacturerSearchModel model)
        {
            if (model == null)
                throw new ServiceException();

            return await QueryByRequest(model).CountAsync();
        }

        public async Task<IList<Manufacturer>> GetByRequestAsync(ManufacturerSearchModel model)
        {
            if (model == null)
                throw new ServiceException();

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var manufaturer = await FindByIdAsync(id);
            if (manufaturer == null)
                throw new ServiceException();

            _manufacturerRepository.Remove(manufaturer);
            await _unitOfWork.SaveAllChangesAsync();
        }

        #endregion Public Methods
    }
}