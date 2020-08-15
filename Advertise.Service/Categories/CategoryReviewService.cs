using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Categories;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.Html;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Categories;
using Advertise.Core.Model.Common;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Categories
{
    public class CategoryReviewService : ICategoryReviewService
    {
        #region Private Fields

        private readonly IDbSet<CategoryReview> _categoryReviewRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CategoryReviewService(IMapper mapper, IUnitOfWork unitOfWork, IEventPublisher eventPublisher, IWebContextManager webContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _webContextManager = webContextManager;
            _categoryReviewRepository = unitOfWork.Set<CategoryReview>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(CategoryReviewSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).CountAsync();
        }

        public async Task CreateByViewModelAsync(CategoryReviewCreateModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException(nameof(viewModel));

            var categoryReview = _mapper.Map<CategoryReview>(viewModel);
            categoryReview.Body = categoryReview.Body.ToSafeHtml();
            categoryReview.IsActive = true;
            categoryReview.CreatedById = _webContextManager.CurrentUserId;
            categoryReview.CreatedOn = DateTime.Now;
            _categoryReviewRepository.Add(categoryReview);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(categoryReview);
        }

        public async Task DeleteByIdAsync(Guid categoryReviewId)
        {
            var categoryReview = await _categoryReviewRepository.FirstOrDefaultAsync(model => model.Id == categoryReviewId);
            _categoryReviewRepository.Remove(categoryReview);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(categoryReview);
        }

        public async Task EditByViewModelAsync(CategoryReviewEditModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var categoryReview = await _categoryReviewRepository.AsNoTracking().FirstOrDefaultAsync(model => model.Id == viewModel.Id);
            _mapper.Map(viewModel, categoryReview);
            categoryReview.Body = categoryReview.Body.ToSafeHtml();
            categoryReview.CategoryId = viewModel.CategoryId;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(categoryReview);
        }

        public async Task<CategoryReview> FindByIdAsync(Guid categoryReviewId)
        {
            return await _categoryReviewRepository
                  .FirstOrDefaultAsync(model => model.Id == categoryReviewId);
        }

        public async Task<IList<CategoryReview>> GetByCategoryIdAsync(Guid categoryId)
        {
            return await _categoryReviewRepository
                .AsNoTracking()
                .Where(model => model.CategoryId == categoryId && model.IsActive.Value)
                .ToListAsync();
        }

        public async Task<IList<CategoryReview>> GetByRequestAsync(CategoryReviewSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).ToPagedListAsync(request.PageIndex, request.PageSize);
        }

        public IQueryable<CategoryReview> QueryByRequest(CategoryReviewSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var categoryReviews = _categoryReviewRepository.AsNoTracking().AsQueryable()
                .Include(model => model.CreatedBy)
                .Include(model => model.CreatedBy.Meta)
                .Include(model => model.Category);
            if (request.Term.HasValue())
                categoryReviews = categoryReviews.Where(model => model.Title.Contains(request.Term) || model.Body.Contains(request.Term));
            if (request.IsActive.HasValue)
                if (request.IsActive.Value.ToString() != "-1")
                    categoryReviews = categoryReviews.Where(model => model.IsActive == request.IsActive);
            if (request.CreatedById.HasValue)
                categoryReviews = categoryReviews.Where(model => model.CreatedById == request.CreatedById);
            if (string.IsNullOrEmpty(request.SortMember))
                request.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(request.SortDirection))
                request.SortDirection = SortDirection.Desc;
            categoryReviews = categoryReviews.OrderBy($"{request.SortMember} {request.SortDirection}");

            return categoryReviews;
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}