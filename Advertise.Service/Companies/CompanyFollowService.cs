using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Domain.Users;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Companies
{
    public class CompanyFollowService : ICompanyFollowService
    {
        #region Private Fields

        private readonly IDbSet<CompanyFollow> _companyFollowRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CompanyFollowService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _companyFollowRepository = unitOfWork.Set<CompanyFollow>();
        }

        #endregion Public Constructors

        #region Public Methods
        
        public async Task<int> CountAllFollowByCompanyIdAsync(Guid companyId)
        {
            var request = new CompanyFollowSearchModel
            {
                CompanyId = companyId,
            };
            var result = await CountByRequestAsync(request);

            return result;
        }

        public async Task<int> CountAsync(Guid comapnyId)
        {
            var request = new CompanyFollowSearchModel
            {
                IsFollow = true,
                PageSize = PageSize.All,
                CompanyId = comapnyId
            };
            var count = await CountByRequestAsync(request);

            return count;
        }

        public async Task<int> CountByRequestAsync(CompanyFollowSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyFollows = QueryByRequest(model);
            var companyFollowsCount = await companyFollows.CountAsync();

            return companyFollowsCount;
        }

        public async Task<CompanyFollow> FindAsync(Guid companyFollowId)
        {
            var companyFollow = await _companyFollowRepository
                .FirstOrDefaultAsync(model => model.Id == companyFollowId);

            return companyFollow;
        }

        public async Task<CompanyFollow> FindByCompanyIdAsync(Guid userId, Guid companyId)
        {
            var companyLike = await _companyFollowRepository
                .FirstOrDefaultAsync(model => model.FollowedById == userId && model.CompanyId == companyId);

            return companyLike;
        }

        public async Task<CompanyFollow> FindByUserIdAsync(Guid userId)
        {
            var companyLike = await _companyFollowRepository
                .FirstOrDefaultAsync(model => model.FollowedById == userId);

            return companyLike;
        }

        public async Task<IList<CompanyFollow>> GetByRequestAsync(CompanyFollowSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyFollows = await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);

            return companyFollows;
        }

        public async Task<IList<User>> GetUsersByCompanyIdAsync(Guid companyId)
        {
            var users = await _companyFollowRepository
                .AsNoTracking()
                .Include(model => model.FollowedBy)
                .Where(model => model.CompanyId == companyId && model.IsFollow == true)
                .Select(model => model.FollowedBy)
                .ToListAsync();

            return users;
        }

        public async Task<bool> IsFollowByCurrentUserAsync(Guid companyId)
        {
            var isFollow = await _companyFollowRepository
                .AsNoTracking()
                .AnyAsync(model => model.CompanyId == companyId && model.FollowedById == _webContextManager.CurrentUserId);

            return isFollow;
        }

        public async Task<bool> IsFollowByUserIdAsync(Guid companyId, Guid userId)
        {
            var isFollow = await _companyFollowRepository
                .AsNoTracking()
                .AnyAsync(model => model.CompanyId == companyId && model.FollowedById == userId && model.IsFollow == true);

            return isFollow;
        }

        public async Task<CompanyFollowListModel> ListByRequestAsync(CompanyFollowSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            if (isCurrentUser)
                model.FollowedById = _webContextManager.CurrentUserId;
            else if (userId != null)
                model.FollowedById = userId;
            else
                model.FollowedById = null;
            model.TotalCount = await CountByRequestAsync(model);
            var companyFollows = await GetByRequestAsync(model);
            var companyFollowViewModel = _mapper.Map<IList<CompanyFollowModel>>(companyFollows);
            var companyFollowList = new CompanyFollowListModel
            {
                SearchModel = model,
                CompanyFollows = companyFollowViewModel
            };

            return companyFollowList;
        }

        public IQueryable<CompanyFollow> QueryByRequest(CompanyFollowSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyFollows = _companyFollowRepository.AsNoTracking().AsQueryable()
                .Include(m => m.FollowedBy)
                .Include(m => m.FollowedBy.Meta)
                .Include(m => m.Company);
            if (model.CompanyId.HasValue)
                companyFollows = companyFollows.Where(m => m.CompanyId == model.CompanyId);
            if (model.FollowedById.HasValue)
                companyFollows = companyFollows.Where(m => m.FollowedById == model.FollowedById);
            if (model.IsFollow.HasValue)
                companyFollows = companyFollows.Where(m => m.IsFollow == model.IsFollow);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            companyFollows = companyFollows.OrderBy($"{model.SortMember} {model.SortDirection}");

            return companyFollows;
        }
        
        public async Task RemoveRangeAsync(IList<CompanyFollow> companyFollows)
        {
            if (companyFollows == null)
                throw new ArgumentNullException(nameof(companyFollows));

            foreach (var companyFollow in companyFollows)
                _companyFollowRepository.Remove(companyFollow);
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task ToggleFollowCurrentUserByCompanyIdAsync(Guid companyId)
        {
            var companyFollow = await FindByCompanyIdAsync(_webContextManager.CurrentUserId, companyId);
            if (companyFollow != null)
            {
                companyFollow.IsFollow = companyFollow.IsFollow != null && !companyFollow.IsFollow.Value;

                await _unitOfWork.SaveAllChangesAsync();

                _eventPublisher.EntityUpdated(companyFollow);
            }
            else
            {
                var newcompanyfollow = new CompanyFollow
                {
                    CompanyId = companyId,
                    IsFollow = true,
                    FollowedById = _webContextManager.CurrentUserId
                };
                _companyFollowRepository.Add(newcompanyfollow);

                await _unitOfWork.SaveAllChangesAsync();

                _eventPublisher.EntityInserted(newcompanyfollow);
            }
        }

        #endregion Public Methods
    }
}