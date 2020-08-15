using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Guarantees;
using Advertise.Core.Exceptions;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Guarantees;
using Advertise.Core.Objects;
using Advertise.Data.DbContexts;
using Advertise.Service.Categories;
using AutoMapper;

namespace Advertise.Service.Guarantees
{
    public class GuaranteeService : IGuaranteeService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IDbSet<Guarantee> _guaranteeRepository;
        private readonly ICategoryService _categoryService;
        private readonly IWebContextManager _webContextManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public GuaranteeService(IUnitOfWork unitOfWork, IEventPublisher eventPublisher, IMapper mapper, ICategoryService categoryService, IWebContextManager webContextManager)
        {
            _unitOfWork = unitOfWork;
            _guaranteeRepository = unitOfWork.Set<Guarantee>();
            _eventPublisher = eventPublisher;
            _mapper = mapper;
            _categoryService = categoryService;
            _webContextManager = webContextManager;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(GuaranteeSearchModel model)
        {
            if (model == null)
                throw new ServiceException();

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(GuaranteeCreateModel model)
        {
            var manufaturer = _mapper.Map<Guarantee>(model);
            _guaranteeRepository.Add(manufaturer);
            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var manufaturer = await FindByIdAsync(id);
            if (manufaturer == null)
                throw new ServiceException();

            _guaranteeRepository.Remove(manufaturer);
            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task EditByViewMoodelAsync(GuaranteeEditModel model)
        {
            var manufaturer = await FindByIdAsync(model.Id);
            if (manufaturer == null)
                throw new ServiceException();

            _mapper.Map(model, manufaturer);
            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task<Guarantee> FindByIdAsync(Guid id)
        {
            return await _guaranteeRepository.SingleOrDefaultAsync(model => model.Id == id);
        }

        public async Task<IList<Select2Object>> GetAsSelect2ObjectAsync()
        {
            var subCategories = await _categoryService.GetChildsByIdAsync(_categoryService.CurrentCategoryId);
            return await _guaranteeRepository.AsNoTracking()
                .Select(model => new Select2Object
                {
                    id = model.Id,
                    text = model.Title
                }).ToListAsync();
        }

        public async Task<IList<SelectList>> GetAsSelectListAsync()
        {
            return await _guaranteeRepository.AsNoTracking()
                .Select(model => new SelectList
                {
                    Text = model.Title,
                    Value = model.Id.ToString()
                })
                .ToListAsync();
        }

        public async Task<IList<Guarantee>> GetByRequestAsync(GuaranteeSearchModel model)
        {
            if (model == null)
                throw new ServiceException();

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public IQueryable<Guarantee> QueryByRequest(GuaranteeSearchModel model)
        {
            var manufaturers = _guaranteeRepository.AsNoTracking().AsQueryable();

            if (model.Term.HasValue())
                manufaturers = manufaturers.Where(m => m.Title.Contains(model.Term));
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            return manufaturers.OrderBy($"{model.SortMember} {model.SortDirection}");
        }

        #endregion Public Methods
    }
}