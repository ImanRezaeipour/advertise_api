using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Companies
{
    public class CompanyVisitService : ICompanyVisitService
    {
        #region Private Fields

        private readonly IDbSet<CompanyVisit> _companyVisitRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;
        private readonly IEventPublisher _eventPublisher;

        #endregion Private Fields

        #region Public Constructors

        public CompanyVisitService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _companyVisitRepository = unitOfWork.Set<CompanyVisit>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByCompanyIdAsync(Guid companyId)
        {
            if (companyId == null)
                throw new ArgumentException(nameof(companyId));

            var request = new CompanyVisitSearchModel
            {
                CompanyId = companyId
            };
            var count = await CountByRequestAsync(request);

            return count;
        }

        public async Task<int> CountByRequestAsync(CompanyVisitSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyVisites = await QueryByRequest(model).CountAsync();

            return companyVisites;
        }

        public async Task  CreateByCompanyIdAsync(Guid companyId)
        {
            if (companyId == null)
                throw new ArgumentException(nameof(companyId));

            var companyVisit = new CompanyVisit
            {
                CompanyId = companyId,
                IsVisit = true,
                CreatedById = _webContextManager.CurrentUserId

            };
            _companyVisitRepository.Add(companyVisit);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(companyVisit);
        }

        public async Task<CompanyVisit> FindAsync(Guid companyVisitId)
        {
            var companyVisit = await _companyVisitRepository
                .FirstOrDefaultAsync(model => model.Id == companyVisitId);

            return companyVisit;
        }

        public async Task<CompanyVisit> FindByCompanyIdAsync(Guid companyId)
        {
            var companyVisit = await _companyVisitRepository
                .FirstOrDefaultAsync(model => model.CompanyId == companyId);

            return companyVisit;
        }

        public IQueryable<CompanyVisit> QueryByRequest(CompanyVisitSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyVisites = _companyVisitRepository.AsNoTracking().AsQueryable();
            if (model.CreatedById.HasValue)
                companyVisites = companyVisites.Where(companyVisit => companyVisit.CreatedById == model.CreatedById);
            if (model.CompanyId.HasValue)
                companyVisites = companyVisites.Where(companyVisit => companyVisit.CompanyId == model.CompanyId);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            companyVisites = companyVisites.OrderBy($"{model.SortMember} {model.SortDirection}");

            return companyVisites;
        }

        public async Task<IList<CompanyVisit>> GetByRequestAsync(CompanyVisitSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyVisites =  await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
           
            return companyVisites;
        }

        public async Task<CompanyVisitListModel> ListByRequestAsync(CompanyVisitSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            if (isCurrentUser)
                model.CreatedById = _webContextManager.CurrentUserId;
            else if (userId != null)
                model.CreatedById = userId;
            else model.CreatedById = null;
            model.TotalCount = await CountByRequestAsync(model);
            var companyVisites = await GetByRequestAsync(model);
            var companyVisitesViewModel = _mapper.Map<IList<CompanyVisitModel>>(companyVisites);
            var listViewModel = new CompanyVisitListModel
            {
                SearchModel = model,
                CompanyVisits = companyVisitesViewModel
            };

            return listViewModel;
        }

        public async Task  SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}