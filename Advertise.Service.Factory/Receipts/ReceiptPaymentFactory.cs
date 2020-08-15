using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Receipts;
using Advertise.Service.Common;
using Advertise.Service.Receipts;
using AutoMapper;

namespace Advertise.Service.Factory.Receipts
{
    public class ReceiptPaymentFactory : IReceiptPaymentFactory
    {
        #region Private Fields

        private readonly IReceiptPaymentService _receiptPaymentService;
        private readonly IReceiptService _receiptService;
        private readonly ICommonService _commonService;
        private readonly IMapper _mapper;
        private readonly IListService _listService;

        #endregion Private Fields

        #region Public Constructors

        public ReceiptPaymentFactory(IReceiptService receiptService, IReceiptPaymentService receiptPaymentService, ICommonService commonService, IMapper mapper, IListService listService)
        {
            _receiptService = receiptService;
            _receiptPaymentService = receiptPaymentService;
            _commonService = commonService;
            _mapper = mapper;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ReceiptPaymentCompleteModel> PrepareCompleteModelAsync(string authority)
        {
            var payment = await _receiptPaymentService.FindByAuthorityCodeAsync(authority);
            if (payment == null)
                return null;

            var reciept = await _receiptService.FindByIdAsync(payment.ReceiptId.GetValueOrDefault());
            if (reciept == null)
                return null;

            var viewModel = new ReceiptPaymentCompleteModel
            {
                InvoiceNumber = reciept.InvoiceNumber,
            };
            if (payment.IsComplete == true & payment.IsSuccess == true)
            {
                viewModel.Message = "پرداخت شما با موفقیت انجام شده است.";
                viewModel.Color = "Green";
                return viewModel;
            }
            if (payment.IsComplete == true & payment.IsSuccess == false)
            {
                viewModel.Message = "متاسفانه پرداخت شما توسط درگاه بانکی با موفقیت انجام نشده است";
                viewModel.Color = "Red";
                return viewModel;
            }
            viewModel.Message = "پرداخت شما کامل نشده است، لطفا نسبت به تکمیل آن اقدام نمائید";
            viewModel.Color = "Blue";

            return viewModel;
        }

        public async Task<ReceiptPaymentListModel> PrepareListModelAsync(ReceiptPaymentSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _receiptPaymentService.CountByRequestAsync(model);
            var receiptPayments = await _receiptPaymentService.GetByRequestAsync(model);
            var receiptPaymentsViewModel = _mapper.Map<List<ReceiptPaymentModel>>(receiptPayments);
            var listViewModel = new ReceiptPaymentListModel
            {
                ReceiptPayments = receiptPaymentsViewModel,
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