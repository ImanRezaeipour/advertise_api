using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Advertise.Core.Common;
using Advertise.Core.Domain.Plans;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Plans;
using Advertise.Data.DbContexts;
using AutoMapper;
using SortDirection = Advertise.Core.Common.SortDirection;

namespace Advertise.Service.Plans
{
    public class PlanService : IPlanService
    {
        #region Private Fields

        private readonly IMapper _mapper;
        private readonly IDbSet<Plan> _planRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public PlanService(IUnitOfWork unitOfWork, IMapper mapper, IWebContextManager webContextManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webContextManager = webContextManager;
            _planRepository = unitOfWork.Set<Plan>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(PlanSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(PlanCreateModel model)
        {
            if (model == null)
                throw new NullReferenceException(nameof(model));

            var plan = _mapper.Map<Plan>(model);
            plan.ModifiedOn = DateTime.Now;
            if (model.PlanDuration != null) plan.DurationDay = model.PlanDuration.Value.ToInt32();
            plan.CreatedById = _webContextManager.CurrentUserId;
            plan.CreatedOn = DateTime.Now;
            _planRepository.Add(plan);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid? planId)
        {
            if (planId == Guid.Empty || planId == null)
                throw new NullReferenceException();

            var plan = await _planRepository.FirstOrDefaultAsync(model => model.Id == planId);
            _planRepository.Remove(plan);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task EditByViewModelAsync(PlanEditModel model)
        {
            if (model == null)
                throw new NullReferenceException();
            var plan = await _planRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, plan);
            if (model.PlanDuration != null) plan.DurationDay = model.PlanDuration.Value.ToInt32();

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task<Plan> FindByCodeAsync(string code)
        {
            return await _planRepository
                .FirstOrDefaultAsync(model => model.Code == code);
        }

        public async Task<Plan> FindByIdAsync(Guid id)
        {
            return await _planRepository
                   .FirstOrDefaultAsync(model => model.Id == id);
        }

        public async Task<IList<SelectList>> GetAllAsSelectListItemAsync()
        {
            var plans = await _planRepository.AsNoTracking()
                .Select(record => new SelectList
                {
                    Value = record.Id.ToString(),
                    Text = record.Title
                })
                .ToListAsync();
            return _mapper.Map<List<SelectList>>(plans);
        }

        public async Task<IList<Plan>> GetByRequestAsync(PlanSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public IQueryable<Plan> QueryByRequest(PlanSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var plans = _planRepository.AsNoTracking().AsQueryable();

            if (model.CreatedById.HasValue)
                plans = plans.Where(m => m.CreatedById == model.CreatedById);
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortDirection = SortMember.ModifiedOn;

            plans = plans.SortBy($"{model.SortDirection} {model.SortMember}");

            return plans;
        }

        #endregion Public Methods
    }
}