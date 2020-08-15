using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Extensions;
using Advertise.Core.Model.Announces;
using Advertise.Service.Announces;
using Advertise.Service.Common;
using AutoMapper;

namespace Advertise.Service.Factory.Announces
{
    public class AnnouncePaymentFactory : IAnnouncePaymentFactory
    {
        #region Private Fields

        private readonly IAnnouncePaymentService _announcePaymentService;
        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public AnnouncePaymentFactory(ICommonService commonService, IMapper mapper, IAnnouncePaymentService adsPaymentService, IListService listService)
        {
            _commonService = commonService;
            _mapper = mapper;
            _listService = listService;
            _announcePaymentService = adsPaymentService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<AnnouncePaymentListModel> PrepareListModel(AnnouncePaymentSearchModel request, bool isCurrentUser = false, Guid? userId = null)
        {
            request.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            var requestBanner = new AnnouncePaymentSearchModel
            {
                CreatedById = request.CreatedById,
                IsApprove = request.IsApprove,
            };
            var list = await _announcePaymentService.QueryByRequest(requestBanner)
                .Include(model => model.Announce)
                .Where(model => model.Announce.IsApprove == true || model.IsComplete == true && model.IsSuccess == true)
                .ToPagedListAsync(request.PageIndex, request.PageSize);
            var bannerPayments = _mapper.Map<IList<AnnouncePaymentModel>>(list);
            request.TotalCount = await _announcePaymentService.CountByRequestAsync(request);

            var adsViewModel = new AnnouncePaymentListModel
            {
                Announces = bannerPayments,
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SearchRequest = request
            };

            if (isCurrentUser)
            {
                adsViewModel.IsMine = true;
                adsViewModel.Announces.ForEach(p => p.IsMine = true);
            }

            return adsViewModel;
        }

        #endregion Public Methods
    }
}