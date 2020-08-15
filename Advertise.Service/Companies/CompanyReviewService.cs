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
using Advertise.Core.Managers.Html;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Core.Types;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Companies
{
    public class CompanyReviewService : ICompanyReviewService
    {
        #region Private Fields

        private readonly IDbSet<Company> _companyRepository;
        private readonly IDbSet<CompanyReview> _companyReviewRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CompanyReviewService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _companyRepository = unitOfWork.Set<Company>();
            _companyReviewRepository = unitOfWork.Set<CompanyReview>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(CompanyReviewSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
            }

        public async Task CreateByViewModelAsync(CompanyReviewCreateModel model)
        {
            if (model == null)
                throw new ArgumentException(nameof(model));

            var companyReview = _mapper.Map<CompanyReview>(model);
            companyReview.Body = companyReview.Body.ToSafeHtml();
            companyReview.State = StateType.Pending;
            companyReview.CreatedOn = DateTime.Now;
            _companyReviewRepository.Add(companyReview);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(companyReview);
        }

        public async Task DeleteByIdAsync(Guid companyReviewId)
        {
            var review = await _companyReviewRepository.FirstOrDefaultAsync(model => model.Id == companyReviewId);
            _companyReviewRepository.Remove(review);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(review);
        }

        public async Task ApproveByViewModelAsync(CompanyReviewEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyReview = await EditAsync(model);

            companyReview.ApprovedById = _webContextManager.CurrentUserId;
            companyReview.State = StateType.Approved;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(companyReview);
        }

        public async Task EditByViewModelAsync(CompanyReviewEditModel model)
        {
            if (model == null)
                throw new ArgumentException(nameof(model));

            var companyReview = await EditAsync(model);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(companyReview);
        }

        public async Task<CompanyReview> EditAsync(CompanyReviewEditModel model)
        {
            var companyReview = await _companyReviewRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, companyReview);
            companyReview.Body = companyReview.Body.ToSafeHtml();
            return companyReview;
        }

        public async Task RejectByViewModelAsync(CompanyReviewEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyReview = await EditAsync(model);
            companyReview.State = StateType.Rejected;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(companyReview);
        }

        public async Task<CompanyReview> FindByIdAsync(Guid companyReviewId)
        {
            return await _companyReviewRepository
                .FirstOrDefaultAsync(model => model.Id == companyReviewId);
        }

        public async Task<IList<CompanyReview>> GetByRequestAsync(CompanyReviewSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<IList<SelectList>> GetCompanyAsSelectListItemAsync()
        {
            var list = await _companyRepository
                .AsNoTracking()
                .Where(model => model.State == StateType.Approved).ToListAsync();
           return  list.Select(item => new SelectList
            {
                Text = item.Title,
                Value = item.Id.ToString()
            }).ToList();
        }

        public async Task<IList<CompanyReview>> GetByCompanyIdAsync(Guid companyId,StateType? state= null)
        {
            var request = new CompanyReviewSearchModel
            {
                CompanyId = companyId,
                State = state
            };
            var list = await GetByRequestAsync(request);

            return list;
        }

        public IQueryable<CompanyReview> QueryByRequest(CompanyReviewSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyReviews = _companyReviewRepository.AsNoTracking().AsQueryable()
                .Include(m => m.Company);
            if (model.CompanyId.HasValue)
                companyReviews = companyReviews.Where(m => m.CompanyId == model.CompanyId);
            if (model.State.HasValue)
                if (model.State != StateType.All)
                    companyReviews = companyReviews.Where(m => m.State == model.State);
            if (model.IsActive.HasValue)
                if (model.IsActive == false || model.IsActive == true)
                    companyReviews = companyReviews.Where(m => m.IsActive == model.IsActive);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            companyReviews = companyReviews.OrderBy($"{model.SortMember} {model.SortDirection}");

            return companyReviews;
        }

        public async Task RemoveRangeAsync(IList<CompanyReview> companyReviews)
        {
            if (companyReviews == null)
                throw new ArgumentNullException(nameof(companyReviews));

            foreach (var companyReview in companyReviews)
                _companyReviewRepository.Remove(companyReview);
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}