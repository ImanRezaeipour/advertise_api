using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Announces;
using Advertise.Core.Model.Announces;
using Advertise.Core.Model.Common;
using Advertise.Data.DbContexts;

namespace Advertise.Service.Announces
{
    public class AnnounceReserveService : IAnnounceReserveService
    {
        #region Private Fields

        private readonly IAnnounceOptionService _announceOptionService;
        private readonly IDbSet<AnnounceReserve> _announceReserveRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public AnnounceReserveService(IUnitOfWork unitOfWork, IAnnounceOptionService announceOptionService)
        {
            _unitOfWork = unitOfWork;
            _announceOptionService = announceOptionService;
            _announceReserveRepository = unitOfWork.Set<AnnounceReserve>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(AnnounceReserveSearchModel request)
        {
            return await QueryByRequest(request).CountAsync();
        }

        public async Task CreateByViewModelAsync(AnnounceReserveCreateModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (viewModel.AdsOptionId == null)
                throw new ArgumentNullException(nameof(viewModel));

            var day = await GetReserveDayByOptionIdAsync(viewModel.AdsOptionId.Value);
            if(day == null)
                return;

            for (var duration = 0; duration < viewModel.DurationType.ToInt32(); duration++)
            {
                var adsReserve = new AnnounceReserve
                {
                    Day = day.Value.AddDays(duration),
                    AnnounceId = viewModel.AdsId
                };

                _announceReserveRepository.Add(adsReserve);
            }

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task<DateTime?> GetReserveDayByOptionIdAsync(Guid optionId)
        {
            var capacity = await _announceOptionService.GetCapacityByIdAsync(optionId);
            if (capacity == 0)
                return null;

            for (var day = DateTime.Now.AddDays(1); day < DateTime.Now.AddDays(30); day = day.AddDays(1))
            {
                var request = new AnnounceReserveSearchModel
                {
                    OptionId = optionId,
                    Day = day
                };
                var reservedCountForDate = await CountByRequestAsync(request);
                if (reservedCountForDate < capacity)
                    return day;
            }
            return null;
        }

        public IQueryable<AnnounceReserve> QueryByRequest(AnnounceReserveSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var adsReserve = _announceReserveRepository.AsNoTracking().AsQueryable()
                .Include(model => model.Announce);

            if (request.Day.HasValue)
                adsReserve = adsReserve.Where(reserve => reserve.Day == request.Day);
            if (request.OptionId.HasValue)
                adsReserve = adsReserve.Where(reserve => reserve.Announce.AnnounceOptionId == request.OptionId);

            adsReserve = adsReserve.OrderBy($"{request.SortMember ?? SortMember.CreatedOn} {request.SortDirection ?? SortDirection.Asc}");

            return adsReserve;
        }

        #endregion Public Methods
    }
}