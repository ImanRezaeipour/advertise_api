using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Domain.Logs;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Logs;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Logs
{
    public class LogActivityService : ILogActivityService
    {
        #region Private Fields

        private readonly IDbSet<LogActivity> _activityLogRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public LogActivityService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _activityLogRepository = unitOfWork.Set<LogActivity>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(LogActivitySearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).CountAsync();
        }

        public async Task CreateByViewModelAsync(LogActivityCreateModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException(nameof(viewModel));

            var activityLog = _mapper.Map<LogActivity>(viewModel);
            activityLog.CreatedById = _webContextManager.CurrentUserId;
            _activityLogRepository.Add(activityLog);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(activityLog);
        }

        public async Task DeleteByIdAsync(Guid activityLogId)
        {
            var activityLog = await _activityLogRepository.FirstOrDefaultAsync(model => model.Id == activityLogId);
            _activityLogRepository.Remove(activityLog);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(activityLog);
        }

        public async Task EditByViewModelAsync(LogActivityEditModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var activityLog = await _activityLogRepository.FirstOrDefaultAsync(model => model.Id == viewModel.Id);
            _mapper.Map(viewModel, activityLog);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(activityLog);
        }

        public async Task<LogActivity> FindByIdAsync(Guid activityLogId)
        {
            return await _activityLogRepository
                  .FirstOrDefaultAsync(model => model.Id == activityLogId);
        }

        public async Task<IList<LogActivity>> GetByRequestAsync(LogActivitySearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).ToPagedListAsync(request.PageIndex, request.PageSize);
        }

        public async Task<LogActivityListModel> ListByRequestAsync(LogActivitySearchModel request, bool isCurrentUser = false, Guid? userId = null)
        {
            if (isCurrentUser)
                request.CreatedById = _webContextManager.CurrentUserId;
            else if (userId != null)
                request.CreatedById = userId;
            else
                request.CreatedById = null;
            request.TotalCount = await CountByRequestAsync(request);
            var activityLogs = await GetByRequestAsync(request);
            var activityLogsViewModel = _mapper.Map<IList<LogActivityModel>>(activityLogs);
            var activityLogsList = new LogActivityListModel
            {
                SearchModel = request,
                ActivityLogs = activityLogsViewModel
            };

            return activityLogsList;
        }

        public IQueryable<LogActivity> QueryByRequest(LogActivitySearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var activityLogs = _activityLogRepository.AsNoTracking().AsQueryable();
            if (request.CreatedById.HasValue)
                activityLogs = activityLogs.Where(m => m.CreatedById == request.CreatedById);
            if (request.Term.HasValue())
                activityLogs = activityLogs.Where(m => m.Title.Contains(request.Term));
            activityLogs = activityLogs.OrderBy($"{request.SortMember} {request.SortDirection}");

            return activityLogs;
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}