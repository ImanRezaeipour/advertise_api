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
using Advertise.Core.Managers.ExportImport;
using Advertise.Core.Managers.ExportImport.Help;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Specifications;
using Advertise.Data.DbContexts;
using Advertise.Service.Categories;
using AutoMapper;
using Specification = Advertise.Core.Domain.Specifications.Specification;

namespace Advertise.Service.Specifications
{
    public class SpecificationService : ISpecificationService
    {
        #region Private Fields

        private readonly ICategoryService _categoryService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<Specification> _specificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public SpecificationService(IMapper mapper, IUnitOfWork unitOfWork, IEventPublisher eventPublisher, ICategoryService categoryService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _categoryService = categoryService;
            _specificationRepository = unitOfWork.Set<Specification>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(SpecificationSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await (await QueryByRequest(model)).CountAsync();
        }

        public async Task CreateByViewModelAsync(SpecificationCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var specification = _mapper.Map<Specification>(model);
            specification.CreatedOn = DateTime.Now;

            if (model.Options == null)
                specification.Options.Clear();
            else
            {
                specification.Options.Clear();
                specification.Options = new HashSet<SpecificationOption>();
                var specificationOptions = _mapper.Map<List<SpecificationOption>>(model.Options);
                foreach (var specificationOption in specificationOptions)
                {
                    specificationOption.CreatedOn = DateTime.Now;
                    specification.Options.Add(specificationOption);
                }
            }

            _specificationRepository.Add(specification);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(specification);
        }

        public async Task DeleteByIdAsync(Guid specificationId)
        {
            var specification = await FindByIdAsync(specificationId);
            _specificationRepository.Remove(specification);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(specification);
        }

        public async Task EditByViewModelAsync(SpecificationEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var specification = await FindByIdAsync(model.Id);
            _mapper.Map(model, specification);

            specification.CategoryId = model.CategoryId;

            if (model.Options == null)
                specification.Options.Clear();
            else
            {
                specification.Options.Clear();
                //specification.Options = new HashSet<SpecificationOption>();
                var specificationOptions = _mapper.Map<List<SpecificationOption>>(model.Options);
                foreach (var specificationOption in specificationOptions)
                {
                    specification.Options.Add(specificationOption);
                }
            }

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(specification);
        }

        public async Task<Specification> FindByIdAsync(Guid specificationId)
        {
            return await _specificationRepository.FirstOrDefaultAsync(model => model.Id == specificationId);
        }

        public async Task<IList<SelectList>> GetAsSelectListItemAsync(Guid categoryId)
        {
            var specificationList = await GetByCategoryIdAsync(categoryId);
            return specificationList.Select(record => new SelectList
            {
                Text = record.Title,
                Value = record.Id.ToString()
            }).ToList();
        }

        public async Task<IList<Specification>> GetByCategoryIdAsync(Guid categoryId)
        {
            var specificationRequest = new SpecificationSearchModel
            {
                PageSize = PageSize.All,
                CategoryId = categoryId,
                WithParentCategory = true,
                SortMember = "Order",
                SortDirection = SortDirection.Asc
            };
            return await GetByRequestAsync(specificationRequest);
        }

        public async Task<IList<Specification>> GetByRequestAsync(SpecificationSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await (await QueryByRequest(model)).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public Guid? GetIdByTitle(string specificationTitle, Guid categoryId)
        {
            var result =   _specificationRepository.AsNoTracking()
                .Where(model => model.Title == specificationTitle && model.CategoryId == categoryId)
                .Select(model => model.Id).FirstOrDefault();

            return result == Guid.Empty ? (Guid?) null : result;
        }

        public async Task<List<string>> GetTitlesAsync()
        {
            return await  _specificationRepository.AsNoTracking().Select(record => record.Title).ToListAsync();
        }

        public async Task<object> GetObjectByCategoryAsync(Guid categoryId)
        {
            var specification = await _specificationRepository
                .AsNoTracking()
                .Where(model => model.CategoryId == categoryId)
                .ToListAsync();

            var viewModel = _mapper.Map<List<SpecificationModel>>(specification);

            return viewModel;
        }

        public async Task<IList<SpecificationModel>> GetViewModelByCategoryAliasAsync(string categoryAlias)
        {
            var category = await _categoryService.FindByAliasAsync(categoryAlias);
            var specification = await _specificationRepository
            .AsNoTracking()
            .Where(model => model.CategoryId == category.Id && model.IsSearchable == true)
            .ToListAsync();

            var viewModel = _mapper.Map<List<SpecificationModel>>(specification);

            return viewModel;
        }

        public async Task<IQueryable<Specification>> QueryByRequest(SpecificationSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var specifications = _specificationRepository.AsNoTracking().AsQueryable()
                .Include(m => m.Category)
                .Include(m => m.Options);

            if (model.CategoryId.HasValue && model.WithParentCategory != true)
                specifications = specifications.Where(m => m.CategoryId == model.CategoryId);
            if (model.CategoryId.HasValue && model.WithParentCategory == true)
            {
                var categoryParentIds = (await _categoryService.GetParentsByIdAsync(model.CategoryId.Value)).Select(m => m.Id);
                specifications = specifications.Where(m => categoryParentIds.Contains(m.CategoryId.Value));
            }
            if (model.Term.HasValue())
                specifications = specifications.Where(m => m.Title.Contains(model.Term) || m.Description.Contains(model.Term));

            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;

            specifications = specifications.OrderBy($"{model.SortMember} {model.SortDirection}");

            return specifications;
        }

        private async Task<byte[]> ExportSpecificationsToXlsxAsync(IEnumerable<SpecificationModel> specifications)
        {
            var properties = new[]
            {
                new PropertyByName<SpecificationModel>("Title", p => p.Title)
            };

            return new ExportManager().ExportToXlsx(properties, specifications);
        }

        #endregion Public Methods
    }
}