using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Categories;
using Advertise.Core.Domain.Users;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Categories;
using Advertise.Core.Model.Common;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Categories
{
    public class CategoryFollowService : ICategoryFollowService
    {
        #region Private Fields

        private readonly IDbSet<CategoryFollow> _categoryFollowRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CategoryFollowService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _categoryFollowRepository = unitOfWork.Set<CategoryFollow>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountAllFollowByCategoryIdAsync(Guid categoryId)
        {
            var request = new CategoryFollowSearchModel
            {
                CategoryId = categoryId,
                PageSize = PageSize.All,
                IsFollow = true
            };
            return await CountByRequestAsync(request);
        }

        public async Task<int> CountByCategoryIdAsync(Guid categoryId)
        {
            return await _categoryFollowRepository
                .AsNoTracking()
                .CountAsync(model => model.CategoryId.GetValueOrDefault() == categoryId && model.IsFollow == true);
        }

        public async Task<int> CountByRequestAsync(CategoryFollowSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).CountAsync();
        }

        public async Task<int> CountByUserIdAsync(Guid userId)
        {
            return await _categoryFollowRepository
                 .AsNoTracking()
                 .CountAsync(model => model.FollowedById == userId && model.IsFollow == true);
        }

        public async Task<CategoryFollow> FindByCategoryIdAsync(Guid categoryId, Guid? userId = null)
        {
            return await _categoryFollowRepository
                 .FirstOrDefaultAsync(model => model.FollowedById == userId && model.CategoryId == categoryId && model.IsFollow == true);
        }

        public async Task<IList<CategoryFollow>> GetByRequestAsync(CategoryFollowSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var categoryFollows = await QueryByRequest(request).ToPagedListAsync(request.PageIndex, request.PageSize);

            return categoryFollows;
        }

        public async Task<IList<User>> GetUsersByCategoryIdAsync(Guid categoryId)
        {
            var users = await _categoryFollowRepository
                .AsNoTracking()
                .Include(model => model.FollowedBy)
                .Where(model => model.CategoryId == categoryId && model.IsFollow.Value).Select(model => model.FollowedBy)
                .ToListAsync();

            return users;
        }

        public async Task<bool> IsFollowCurrentUserByCategoryIdAsync(Guid categoryId)
        {
            return await _categoryFollowRepository
                .AsNoTracking()
                .AnyAsync(model => model.CategoryId == categoryId && model.FollowedById == _webContextManager.CurrentUserId);
        }

        public async Task<bool> IsFollowByCategoryIdAsync(Guid categoryId, Guid? userId = null)
        {
            return await _categoryFollowRepository
                .AsNoTracking()
                .AnyAsync(model => model.CategoryId == categoryId && model.FollowedById == userId && model.IsFollow == true);
        }

        public IQueryable<CategoryFollow> QueryByRequest(CategoryFollowSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var categoryFollows = _categoryFollowRepository.AsNoTracking().AsQueryable()
                .Include(model => model.FollowedBy)
                .Include(model => model.FollowedBy.Meta)
                .Include(model => model.Category);
            if (request.FollowedById.HasValue)
                categoryFollows = categoryFollows.Where(model => model.FollowedById == request.FollowedById);
            if (request.CategoryId.HasValue)
                categoryFollows = categoryFollows.Where(model => model.CategoryId == request.CategoryId);
            if (request.FollowedById.HasValue)
                categoryFollows = categoryFollows.Where(model => model.FollowedById == request.FollowedById);
            if (request.IsFollow.HasValue)
                categoryFollows = categoryFollows.Where(model => model.IsFollow == request.IsFollow);
            if (string.IsNullOrEmpty(request.SortMember))
                request.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(request.SortDirection))
                request.SortDirection = SortDirection.Desc;
            categoryFollows = categoryFollows.OrderBy($"{request.SortMember} {request.SortDirection}");

            return categoryFollows;
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task ToggleFollowCurrentUserByCategoryIdAsync(Guid categoryId)
        {
            var categoryFollow = await FindByCategoryIdAsync(categoryId, _webContextManager.CurrentUserId);
            if (categoryFollow != null)
            {
                categoryFollow.IsFollow = !categoryFollow.IsFollow;

                await _unitOfWork.SaveAllChangesAsync();

                _eventPublisher.EntityUpdated(categoryFollow);
            }
            else
            {
                var newCategoryfollow = new CategoryFollow
                {
                    CategoryId = categoryId,
                    IsFollow = true,
                    FollowedById = _webContextManager.CurrentUserId
                };
                _categoryFollowRepository.Add(newCategoryfollow);

                await _unitOfWork.SaveAllChangesAsync();

                _eventPublisher.EntityInserted(newCategoryfollow);
            }
        }

        #endregion Public Methods
    }
}