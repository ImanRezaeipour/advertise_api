using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Companies
{
    public class CompanyRateService : ICompanyRateService
    {
        #region Private Fields

        private readonly IDbSet<CompanyRate> _companyRateRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CompanyRateService(IUnitOfWork unitOfWork, IMapper mapper, IEventPublisher eventPublisher, IWebContextManager webContextManager)
        {
            _unitOfWork = unitOfWork;
            _companyRateRepository = unitOfWork.Set<CompanyRate>();
            _mapper = mapper;
            _eventPublisher = eventPublisher;
            _webContextManager = webContextManager;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(CompanyRateSearchModel model)
        {
            var count = await QueryByRequest(model).CountAsync();

            return count;
        }

        public async Task CreateByViewModelAsync(CompanyRateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyRate = _mapper.Map<CompanyRate>(model);
            companyRate.CreatedById = _webContextManager.CurrentUserId;
            _companyRateRepository.Add(companyRate);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(companyRate);
        }

        public IQueryable<CompanyRate> QueryByRequest(CompanyRateSearchModel model)
        {
            var companyRates = _companyRateRepository.AsNoTracking().AsQueryable();

            if (model.CreatedById != null)
                companyRates = companyRates.Where(m => m.CreatedById == model.CreatedById);
            if (model.CompanyId != null)
                companyRates = companyRates.Where(m => m.CompanyId == model.CompanyId);

            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Asc;

            companyRates = companyRates.OrderBy($"{model.SortMember} {model.SortDirection}");

            return companyRates;
        }

        public async Task<decimal> RateByRequestAsync(CompanyRateSearchModel model)
        {
            var rates = await QueryByRequest(model).SumAsync(m => m.Rate.ToInt32());
            var counts = await QueryByRequest(model).CountAsync();
            var rate = rates / counts;

            return rate;
        }

        #endregion Public Methods
    }
}