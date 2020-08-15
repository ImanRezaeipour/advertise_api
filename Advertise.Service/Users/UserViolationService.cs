using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Users;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Users;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Users
{
    public class UserViolationService : IUserViolationService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<UserViolation> _userViolationRepository;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public UserViolationService(IMapper mapper, IUnitOfWork unitOfWork, IEventPublisher eventPublisher, IWebContextManager webContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _webContextManager = webContextManager;
            _userViolationRepository = unitOfWork.Set<UserViolation>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(UserViolationSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(UserViolationCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var userViolation = _mapper.Map<UserViolation>(model);
            userViolation.ReportedById = _webContextManager.CurrentUserId;
            userViolation.CreatedOn = DateTime.Now;
            _userViolationRepository.Add(userViolation);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(userViolation);
        }

        public async Task DeleteByIdAsync(Guid userViolationId)
        {
            if (userViolationId == null)
                throw new ArgumentNullException(nameof(userViolationId));

            var userViolation = await FindByIdAsync(userViolationId);
            _userViolationRepository.Remove(userViolation);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(userViolation);
        }

        public async Task EditByViewModelAsync(UserViolationEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var userViolation = await FindByIdAsync(model.Id);
            _mapper.Map(model, userViolation);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(userViolation);
        }

        public async Task<UserViolation> FindByIdAsync(Guid userViolationId)
        {
            return await _userViolationRepository.FirstOrDefaultAsync(model => model.Id == userViolationId);
        }

        public async Task<IList<UserViolation>> GetByRequestAsync(UserViolationSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public IQueryable<UserViolation> QueryByRequest(UserViolationSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var userViolations = _userViolationRepository.AsNoTracking().AsQueryable();

            if (model.Term.HasValue())
                userViolations = userViolations.Where(m => m.ReasonDescription.Contains(model.Term));
            if (model.IsRead.HasValue)
                userViolations = userViolations.Where(m => m.IsRead == model.IsRead);
            if (model.ReasonType.HasValue)
                userViolations = userViolations.Where(m => m.Reason == model.ReasonType);
            if (model.ReportType.HasValue)
                userViolations = userViolations.Where(m => m.Type == model.ReportType);

            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;

            userViolations = userViolations.OrderBy($"{model.SortMember} {model.SortDirection}");

            return userViolations;
        }

        #endregion Public Methods
    }
}