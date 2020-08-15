using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Plans;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Plans;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Plans
{
    public class PlanDiscountService : IPlanDiscountService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<PlanDiscount> _planDiscountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public PlanDiscountService(IMapper mapper, IUnitOfWork unitOfWork, IEventPublisher eventPublisher, IWebContextManager webContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _webContextManager = webContextManager;
            _planDiscountRepository = _unitOfWork.Set<PlanDiscount>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(PlanDiscountSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(PlanDiscountCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var planDiscount = _mapper.Map<PlanDiscount>(model);
            planDiscount.CreatedOn = DateTime.Now;
            planDiscount.CreatedById = _webContextManager.CurrentUserId;
            _planDiscountRepository.Add(planDiscount);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(planDiscount);
        }

        public async Task DeleteByIdAsync(Guid? planDiscountId)
        {
            if (planDiscountId == null)
                throw new ArgumentNullException(nameof(planDiscountId));

            var planDiscount = await FindByIdAsync(planDiscountId.Value);
            _planDiscountRepository.Remove(planDiscount);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(planDiscount);
        }

        public async Task EditByViewModelAsync(PlanDiscountEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var planDiscount = await FindByIdAsync(model.Id);
            _mapper.Map(model, planDiscount);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(planDiscount);
        }

        public async Task<PlanDiscount> FindByIdAsync(Guid planDiscountId)
        {
            return await _planDiscountRepository.SingleOrDefaultAsync(model => model.Id == planDiscountId);
        }

        public async Task<IList<PlanDiscount>> GetByRequestAsync(PlanDiscountSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<int?> GetPercentByCodeAsync(string planDiscountCode)
        {
            return await _planDiscountRepository.AsNoTracking()
                .Where(model => model.Code == planDiscountCode)
                .Select(model => model.Percent)
                .SingleOrDefaultAsync();
        }

        public IQueryable<PlanDiscount> QueryByRequest(PlanDiscountSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var planDiscount = _planDiscountRepository.AsNoTracking().AsQueryable();

            if (model.CreatedById.HasValue)
                planDiscount = planDiscount.Where(m => m.CreatedById == model.CreatedById);

            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortDirection = SortMember.CreatedOn;

            planDiscount = planDiscount.OrderBy($"{model.SortMember} {model.SortDirection}");

            return planDiscount;
        }

        #endregion Public Methods
    }
}