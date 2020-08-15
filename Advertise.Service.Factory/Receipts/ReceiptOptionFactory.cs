using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Receipts;
using Advertise.Service.Common;
using Advertise.Service.Receipts;
using AutoMapper;

namespace Advertise.Service.Factory.Receipts
{
    public class ReceiptOptionFactory : IReceiptOptionFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IReceiptOptionService _receiptOptionService;

        #endregion Private Fields

        #region Public Constructors

        public ReceiptOptionFactory(ICommonService commonService, IMapper mapper, IReceiptOptionService receiptOptionService, IListService listService)
        {
            _commonService = commonService;
            _mapper = mapper;
            _receiptOptionService = receiptOptionService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ReceiptOptionListModel> PrepareListModel(ReceiptOptionSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.UserId = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _receiptOptionService.CountByRequestAsync(model);
            var receiptPayments = await _receiptOptionService.GetByRequestAsync(model);
            var receiptPaymentsViewModel = _mapper.Map<List<ReceiptOptionModel>>(receiptPayments);
            var listViewModel = new ReceiptOptionListModel
            {
                ReceiptOptions = receiptPaymentsViewModel,
                SearchModel = model,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
            };

            if (isCurrentUser)
                listViewModel.IsMine = true;

            return listViewModel;
        }

        #endregion Public Methods
    }
}