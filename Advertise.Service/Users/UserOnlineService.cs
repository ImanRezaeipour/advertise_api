using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Users;
using Advertise.Core.Managers.Event;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Users;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Users
{
    public class UserOnlineService : IUserOnlineService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<UserOnline> _userOnlineRepository;

        #endregion Private Fields

        #region Public Constructors

        public UserOnlineService(IUnitOfWork unitOfWork, IMapper mapper, IEventPublisher eventPublisher)
        {
            _unitOfWork = unitOfWork;
            _userOnlineRepository = unitOfWork.Set<UserOnline>();
            _mapper = mapper;
            _eventPublisher = eventPublisher;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountAllAsync()
        {
            var request = new UserOnlineSearchModel
            {
                IsActive = true
            };
            return await CountByRequestAsync(request);
        }

        public async Task<int> CountByRequestAsync(UserOnlineSearchModel model)
        {
            return await QueryByRequest(model).CountAsync();
        }

        public void CreateByViewModel(UserOnlineModel model)
        {
            var userOnline = _mapper.Map<UserOnline>(model);
            _userOnlineRepository.Add(userOnline);

            _unitOfWork.SaveAllChanges();

            _eventPublisher.EntityInserted(userOnline);
        }

        public void DeleteBySessionId(string sessionId)
        {
            var userOnline = _userOnlineRepository.FirstOrDefault(model => model.SessionId == sessionId);
            _userOnlineRepository.Remove(userOnline);

            _unitOfWork.SaveAllChanges();

            _eventPublisher.EntityDeleted(userOnline);
        }

        public IQueryable<UserOnline> QueryByRequest(UserOnlineSearchModel model)
        {
            var userOnlines = _userOnlineRepository.AsNoTracking().AsQueryable();

            if (model.CreatedById != null)
                userOnlines = userOnlines.Where(m => m.CreatedById == model.CreatedById);

            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Asc;

            userOnlines = userOnlines.OrderBy($"{model.SortMember} {model.SortDirection}");

            return userOnlines;
        }

        #endregion Public Methods
    }
}