using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Announces;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Model.Announces;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;
using Advertise.Data.DbContexts;
using Advertise.Data.Validation.Announces;
using Advertise.Data.Validation.Common;
using AutoMapper;

namespace Advertise.Service.Announces
{
    public class AnnounceOptionService : IAnnounceOptionService
    {
        #region Private Fields

        private readonly IDbSet<AnnounceOption> _announceOptionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventPublisher _eventPublisher;
        private readonly IModelValidator _modelValidator;

        #endregion Private Fields

        #region Public Constructors

        public AnnounceOptionService(IUnitOfWork unitOfWork, IMapper mapper, IEventPublisher eventPublisher, IModelValidator modelValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _modelValidator = modelValidator;
            _announceOptionRepository = unitOfWork.Set<AnnounceOption>();
            _eventPublisher = eventPublisher;
            _modelValidator = modelValidator;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(AnnounceOptionSearchModel request)
        {
            _modelValidator.Validate<ObjectValidator, object>(request);

            return await QueryByRequest(request).CountAsync();
        }

        public async Task CreateByModelAsync(AnnounceOptionCreateModel model)
        {
            _modelValidator.Validate<AnnounceOptionCreateValidator, AnnounceOptionCreateModel>(model);

            var announceOption = _mapper.Map<AnnounceOption>(model);
            
            _announceOptionRepository.Add(announceOption);
            await _unitOfWork.SaveAllChangesAsync();
            
            _eventPublisher.EntityInserted(announceOption);
        }

        public async Task DeleteByIdAsync(Guid announceOptionId)
        {
            var announceOption = await FindByIdAsync(announceOptionId);

            _announceOptionRepository.Remove(announceOption);
            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task EditByViewModelAsync(AnnounceOptionEditModel viewModel)
        {
            var announceOption = await FindByIdAsync(viewModel.Id);
            if (announceOption == null)
                throw new Exception(nameof(announceOption));

            _mapper.Map(viewModel, announceOption);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task<AnnounceOption> FindByIdAsync(Guid adsOptionId)
        {
            return await _announceOptionRepository.FirstOrDefaultAsync(model => model.Id == adsOptionId);
        }

        public async Task<IList<SelectList>> GetAsSelectListAsync(AnnounceType type, AnnouncePositionType? position = null)
        {
            var request = new AnnounceOptionSearchModel
            {
                Position = position,
                Type = type
            };
            return await QueryByRequest(request).Select(record => new SelectList
            {
                Value = record.Id.ToString(),
                Text = " تبلیغ " + record.Title + " در جایگاه " + record.PositionType + " به مبلغ " + record.Price
            }).ToListAsync();
        }

        public async Task<IList<AnnounceOption>> GetByRequestAsync(AnnounceOptionSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).ToPagedListAsync(request.PageIndex, request.PageSize);
        }

        public async Task<int> GetCapacityByIdAsync(Guid optionId)
        {
            return (await FindByIdAsync(optionId)).Capacity ?? 0;
        }

        public async Task<decimal> GetPriceByIdAsync(Guid optionId, DurationType durationType)
        {
            var option = await FindByIdAsync(optionId);
            return option.Price.GetValueOrDefault() * durationType.ToInt32();
        }

        public IQueryable<AnnounceOption> QueryByRequest(AnnounceOptionSearchModel request)
        {
            _modelValidator.Validate<ObjectValidator, object>(request);
            
            var announceOption = _announceOptionRepository.AsNoTracking().AsQueryable();

            if (request.Term.HasValue())
                announceOption = announceOption.Where(model => model.Title.Contains(request.Term));
            
            if (request.Position.HasValue)
                announceOption = announceOption.Where(model => model.PositionType == request.Position);
            
            if (request.Type.HasValue)
                announceOption = announceOption.Where(model => model.Type == request.Type);

            if (string.IsNullOrEmpty(request.SortDirection))
                request.SortDirection = SortDirection.Desc;
            
            if (string.IsNullOrEmpty(request.SortMember))
                request.SortMember = SortMember.CreatedOn;

            announceOption = announceOption.OrderBy($"{request.SortMember} {request.SortDirection}");

            return announceOption;
        }

        #endregion Public Methods
    }
}