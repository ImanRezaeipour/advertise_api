using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Specifications;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Specifications;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Specifications
{
    public class SpecificationOptionService : ISpecificationOptionService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<SpecificationOption> _specificationOptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public SpecificationOptionService(IMapper mapper, IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _specificationOptionRepository = unitOfWork.Set<SpecificationOption>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(SpecificationOptionSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(SpecificationOptionCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var specificationOptions = _mapper.Map<List<SpecificationOption>>(model.Options);
            foreach (var option in specificationOptions)
            {
                option.SpecificationId = model.SpecificationId;
            }

            foreach (var specificationOption in specificationOptions)
                _specificationOptionRepository.Add(specificationOption);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid specificationOptionId)
        {
            var specificationOption = await _specificationOptionRepository.FirstOrDefaultAsync(model => model.Id == specificationOptionId);
            _specificationOptionRepository.Remove(specificationOption);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task EditByViewModelAsync(SpecificationOptionEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var specificationOption = await _specificationOptionRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, specificationOption);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityUpdated(specificationOption);
        }

        public async Task<SpecificationOption> FindByIdAsync(Guid specificationOptionId)
        {
            return await _specificationOptionRepository
                .FirstOrDefaultAsync(model => model.Id == specificationOptionId);
        }

        public async Task<SpecificationOption> FindWithCategoryAsync(Guid specificationOptionId)
        {
            return await _specificationOptionRepository
                .Include(cat => cat.Specification.Category)
                .FirstOrDefaultAsync(model => model.Id == specificationOptionId);
        }

        public async Task<IList<SpecificationOption>> GetByRequestAsync(SpecificationOptionSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<Guid?> GetIdByTitleAsync(string specificationOptionTitle, Guid specificationId)
        {
            return await _specificationOptionRepository.AsNoTracking()
                .Where(model => model.Title == specificationOptionTitle && model.SpecificationId == specificationId)
                .Select(model => model.Id)
                .SingleOrDefaultAsync();
        }

        public Guid? GetIdByTitle(string specificationOptionTitle, Guid specificationId)
        {
            return  _specificationOptionRepository.AsNoTracking()
                .Where(model => model.Title == specificationOptionTitle && model.SpecificationId == specificationId)
                .Select(model => model.Id)
                .SingleOrDefault();
        }
        
        public async Task<IList<SpecificationOption>> GetSpecificationOptionsBySpecificationIdAsync(Guid specificationId)
        {
            return await _specificationOptionRepository
                .AsNoTracking()
                .Where(model => model.SpecificationId == specificationId)
                .ToListAsync();
        }

        public async Task<IList<SelectList>> GetAsSelectListBySpecificationIdAsync(Guid specificationId)
        {
            return await _specificationOptionRepository.AsNoTracking()
                .Where(model => model.SpecificationId == specificationId)
                .Select(model => new SelectList
                {
                    Text = model.Title,
                    Value = model.Id.ToString()
                })
                .ToListAsync();
        }

        public async Task<IList<SpecificationOptionModel>> GetViewModelBySpecificationIdAsync(Guid specificationId)
        {
            var specificationOptions = await _specificationOptionRepository
                .AsNoTracking()
                .Where(model => model.SpecificationId == specificationId)
                .ToListAsync();

            return _mapper.Map<List<SpecificationOptionModel>>(specificationOptions);
        }

        public IQueryable<SpecificationOption> QueryByRequest(SpecificationOptionSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var specificationOptions = _specificationOptionRepository.AsNoTracking().AsQueryable()
                .Include(m => m.Specification);
            if (model.SpecificationId.HasValue)
                specificationOptions = specificationOptions.Where(m => m.SpecificationId == model.SpecificationId);
            if (model.Term.HasValue())
                specificationOptions = specificationOptions.Where(m => m.Description.Contains(model.Term) || m.Title.Contains(model.Term));
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            specificationOptions = specificationOptions.OrderBy($"{model.SortMember} {model.SortDirection}");

            return specificationOptions;
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}