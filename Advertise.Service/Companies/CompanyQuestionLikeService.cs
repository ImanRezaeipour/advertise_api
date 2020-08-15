using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Domain.Users;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Companies;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Companies
{
    public class CompanyQuestionLikeService : ICompanyQuestionLikeService
    {
        #region Private Fields

        private readonly IDbSet<CompanyQuestionLike> _companyQuestionLikeRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CompanyQuestionLikeService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _companyQuestionLikeRepository = unitOfWork.Set<CompanyQuestionLike>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(CompanyQuestionLikeSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyQuestionLikes = await QueryByRequest(model).CountAsync();

            return companyQuestionLikes;
        }

        public async Task<CompanyQuestionLike> FindAsync(Guid companyId, Guid userId)
        {
            var companyQuestionLike = await _companyQuestionLikeRepository
                .FirstOrDefaultAsync(model => model.QuestionId == companyId && model.LikedById == userId);

            return companyQuestionLike;
        }

        public async Task<IList<CompanyQuestionLike>> GetByRequestAsync(CompanyQuestionLikeSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyQuestionLikes = await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);

            return companyQuestionLikes;
        }

        public async Task<IList<User>> GetUsersByCompanyIdAsync(Guid questionId)
        {
            var users = await _companyQuestionLikeRepository
                .AsNoTracking()
                .Include(model => model.LikedById)
                .Where(model => model.QuestionId == questionId && model.IsLike.GetValueOrDefault())
                .Select(model => model.LikedBy)
                .ToListAsync();

            return users;
        }

        public async Task<bool> IsLikeByCurrentUserAsync(Guid questionId)
        {
            var isLike = await _companyQuestionLikeRepository
                .AsNoTracking()
                .AnyAsync(model => model.QuestionId == questionId && model.LikedById == _webContextManager.CurrentUserId && model.IsLike.GetValueOrDefault());

            return isLike;
        }

        public async Task<bool> IsLikeByUserIdAsync(Guid questionId, Guid userId)
        {
            var isLike = await _companyQuestionLikeRepository
                .AsNoTracking()
                .AnyAsync(model => model.QuestionId == questionId && model.LikedById == userId && model.IsLike.GetValueOrDefault());

            return isLike;
        }

        public async Task<CompanyQuestionLikeListModel> ListByRequestAsync(CompanyQuestionLikeSearchModel model)
        {
            model.TotalCount = await CountByRequestAsync(model);
            var companyQuestionLikes = await GetByRequestAsync(model);
            var companyQuestionLikeViewModel = _mapper.Map<IList<CompanyQuestionLikeModel>>(companyQuestionLikes);
            var companyQuestionLikeList = new CompanyQuestionLikeListModel
            {
                SearchModel = model,
                CompanyQuestionLikes = companyQuestionLikeViewModel
            };

            return companyQuestionLikeList;
        }

        public IQueryable<CompanyQuestionLike> QueryByRequest(CompanyQuestionLikeSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyQuestionLikes = _companyQuestionLikeRepository.AsNoTracking().AsQueryable()
                .Include(m => m.LikedBy)
                .Include(m => m.Question);
            if (model.QuestionId.HasValue)
                companyQuestionLikes = companyQuestionLikes.Where(m => m.QuestionId == model.QuestionId);
            if (model.LikedById.HasValue)
                companyQuestionLikes = companyQuestionLikes.Where(m => m.LikedById == model.LikedById);
            companyQuestionLikes = companyQuestionLikes.OrderBy($"{model.SortMember} {model.SortDirection}");

            return companyQuestionLikes;
        }

        public async Task RemoveRangeAsync(IList<CompanyQuestionLike> companyQuestionLikes)
        {
            if (companyQuestionLikes == null)
                throw new ArgumentNullException(nameof(companyQuestionLikes));

            foreach (var companyQuestionLike in companyQuestionLikes)
                _companyQuestionLikeRepository.Remove(companyQuestionLike);
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SetIsLikeByCurrentUserAsync(Guid questionId, bool isLike)
        {
            var companyQuestionLike = await _companyQuestionLikeRepository
                .FirstOrDefaultAsync(model => model.QuestionId == questionId && model.LikedById == _webContextManager.CurrentUserId);
            companyQuestionLike.IsLike = isLike;
        }

        public async Task ToggleLikeByCurrentUserAsync(Guid questionId)
        {
            var companyQuestionLike = await FindAsync(questionId, _webContextManager.CurrentUserId);
            if (companyQuestionLike != null)
            {
                await SetIsLikeByCurrentUserAsync(questionId, !companyQuestionLike.IsLike.GetValueOrDefault());

                await _unitOfWork.SaveAllChangesAsync();

                _eventPublisher.EntityUpdated(companyQuestionLike);
            }

            var newCompanyQuestionLike = new CompanyQuestionLike()
            {
                QuestionId = questionId,
                IsLike = true,
                LikedById = _webContextManager.CurrentUserId
            };
            _companyQuestionLikeRepository.Add(newCompanyQuestionLike);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(newCompanyQuestionLike);
        }

        #endregion Public Methods
    }
}