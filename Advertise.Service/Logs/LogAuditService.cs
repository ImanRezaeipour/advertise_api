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
    public class LogAuditService : ILogAuditService
    {
        #region Private Fields

        private readonly IDbSet<LogAudit> _auditLogRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public LogAuditService(IMapper mapper, IUnitOfWork unitOfWork, IEventPublisher eventPublisher, IWebContextManager webContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _webContextManager = webContextManager;
            _auditLogRepository = unitOfWork.Set<LogAudit>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(LogAuditSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(LogAuditCreateModel model)
        {
            if (model == null)
                throw new ArgumentException(nameof(model));

            var auditLog = _mapper.Map<LogAudit>(model);
            auditLog.CreatedById = _webContextManager.CurrentUserId;
            _auditLogRepository.Add(auditLog);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(auditLog);
        }

        public async Task DeleteByIdAsync(Guid auditLogId)
        {
            var auditLog = await _auditLogRepository.FirstOrDefaultAsync(model => model.Id == auditLogId);
            _auditLogRepository.Remove(auditLog);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(auditLog);
        }

        public async Task EditByViewModelAsync(LogAuditEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var auditLog = await _auditLogRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, auditLog);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(auditLog);
        }

        public async Task<LogAudit> FindByIdAsync(Guid auditLogId)
        {
            return await _auditLogRepository
                  .FirstOrDefaultAsync(model => model.Id == auditLogId);
        }

        public async Task<IList<LogAudit>> GetByRequestAsync(LogAuditSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<LogAuditListModel> ListByRequestAsync(LogAuditSearchModel model)
        {
            model.TotalCount = await CountByRequestAsync(model);
            var auditLogs = await GetByRequestAsync(model);
            var auditLogsViewModel = _mapper.Map<IList<LogAuditModel>>(auditLogs);
            return new LogAuditListModel
            {
                SearchModel = model,
                AuditLogs = auditLogsViewModel
            };
        }

        public IQueryable<LogAudit> QueryByRequest(LogAuditSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var auditLogs = _auditLogRepository.AsNoTracking().AsQueryable();
            if (model.CreatedById.HasValue)
                auditLogs = auditLogs.Where(m => m.CreatedById == model.CreatedById);
            auditLogs = auditLogs.OrderBy($"{model.SortMember} {model.SortDirection}");

            return auditLogs;
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}