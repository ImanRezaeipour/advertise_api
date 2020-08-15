using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Complaints;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Complaints;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Complaints
{
    public class ComplaintService : IComplaintService
    {
        #region Private Fields

        private readonly IDbSet<Complaint> _complaintRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public ComplaintService(IMapper mapper, IUnitOfWork unitOfWork, IEventPublisher eventPublisher, IWebContextManager webContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _webContextManager = webContextManager;
            _complaintRepository = unitOfWork.Set<Complaint>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(ComplaintSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var complaints = await QueryByRequest(model).CountAsync();

            return complaints;
        }

        public async Task CreateByViewModel(ComplaintCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var complaint = _mapper.Map<Complaint>(model);
            complaint.CreatedById = _webContextManager.CurrentUserId;
            complaint.CreatedOn = DateTime.Now;
            _complaintRepository.Add(complaint);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(complaint);
        }

        public async Task DeleteByIdAsync(Guid complaintId)
        {
            var complaint = await _complaintRepository.FirstOrDefaultAsync(model => model.Id == complaintId);
            _complaintRepository.Remove(complaint);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(complaint);
        }

        public async Task<Complaint> FindByIdAsync(Guid complaintId)
        {
            return await _complaintRepository
                   .FirstOrDefaultAsync(model => model.Id == complaintId);
        }

        public async Task<IList<Complaint>> GetByRequestAsync(ComplaintSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public IQueryable<Complaint> QueryByRequest(ComplaintSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var complaints = _complaintRepository.AsNoTracking().AsQueryable();
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            if (model.Term.HasValue())
                complaints = complaints.Where(complaint => complaint.Title.Contains(model.Term));
            complaints = complaints.OrderBy($"{model.SortMember} {model.SortDirection}");

            return complaints;
        }

        #endregion Public Methods
    }
}