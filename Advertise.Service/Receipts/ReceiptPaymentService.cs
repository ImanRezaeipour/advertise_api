using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Configuration;
using Advertise.Core.Domain.Receipts;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.PaymentGateway;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Receipts;
using Advertise.Core.Types;
using Advertise.Data.DbContexts;
using Advertise.Service.Carts;
using Advertise.Service.Common;
using AutoMapper;

namespace Advertise.Service.Receipts
{
    public class ReceiptPaymentService : IReceiptPaymentService
    {
        #region Private Fields

        private readonly ICartService _bagService;
        private readonly IConfigurationManager _configurationManager;
        private readonly IMapper _mapper;
        private readonly IZarinpalGatewayManager _paymentGatewayManager;
        private readonly IDbSet<ReceiptPayment> _receiptPaymentRepository;
        private readonly IReceiptService _receiptService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public ReceiptPaymentService(IMapper mapper, IReceiptService receiptService, IConfigurationManager configurationManager, IZarinpalGatewayManager paymentGatewayManager, ICartService bagService, IUnitOfWork unitOfWork, IWebContextManager webContextManager)
        {
            _mapper = mapper;
            _receiptService = receiptService;
            _configurationManager = configurationManager;
            _paymentGatewayManager = paymentGatewayManager;
            _bagService = bagService;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _receiptPaymentRepository = unitOfWork.Set<ReceiptPayment>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ServiceResult> CallbackByViewModelAsync(ReceiptPaymentCallbackModel model)
        {
            if (string.IsNullOrEmpty(model.Status))
                throw new ArgumentNullException(nameof(model.Status));

            if (string.IsNullOrEmpty(model.Authority))
                throw new ArgumentNullException(nameof(model.Authority));

            long referenceCode;
            var zarinpal = _paymentGatewayManager.ZarinpalGateway();
            var payment = await FindByAuthorityCodeAsync(model.Authority);
            if (payment.Amount != null)
            {
                var status = zarinpal.PaymentVerification(_configurationManager.MerchantCode, payment.AuthorityCode,
                    payment.Amount.Value, out referenceCode);

                payment.ReferenceCode = referenceCode;
                payment.IsComplete = true;
                payment.StatusCode = status;

                switch (status)
                {
                    case 100:
                    case 101:
                        payment.IsSuccess = true;
                        _mapper.Map(model, payment);

                        var result100 = await _unitOfWork.SaveAllChangesAsync();

                        if (result100 != 0)
                        {
                            if (payment.ReceiptId != null)
                            {
                                await _receiptService.SetIsBuyByReceiptIdAsync(payment.ReceiptId.Value, true);
                                await _receiptService.SetInvoiceNumberAsync(payment.ReceiptId.Value,
                                    await _receiptService.GenerateCodeForReceiptAsync());
                                await _bagService.DeleteByCurrentUserAsync();
                                return ServiceResult.Success;
                            }
                        }

                        return ServiceResult.Failed("خطای داخلی پیش آمده است");

                    case -1: // اطلاعات ارسال شده ناقص است
                    case -2: // IP و يا مرچنت كد پذيرنده صحيح نيست
                    case -3: // با توجه به محدوديت هاي شاپرك امكان پرداخت با رقم درخواست شده ميسر نمي باشد
                    case -4: // سطح تاييد پذيرنده پايين تر از سطح نقره اي است
                    case -11: // درخواست مورد نظر يافت نشد
                    case -12: // امكان ويرايش درخواست ميسر نمي باشد
                    case -21: // هيچ نوع عمليات مالي براي اين تراكنش يافت نشد
                    case -22: // تراكنش نا موفق ميباشد
                    case -33: // رقم تراكنش با رقم پرداخت شده مطابقت ندارد.
                    case -34: // سقف تقسيم تراكنش از لحاظ تعداد يا رقم عبور نموده است
                    case -40: // اجازه دسترسي به متد مربوطه وجود ندارد
                    case -41: // اطلاعات ارسال شده مربوط به  AdditionalDataغيرمعتبر ميباشد
                    case -42: // مدت زمان معتبر طول عمر شناسه پرداخت بايد بين  30دقيه تا  45روز مي باشد
                    case -54: // درخواست مورد نظر آرشيو شده است
                        payment.IsSuccess = false;
                        _mapper.Map(model, payment);

                        var result = await _unitOfWork.SaveAllChangesAsync();

                        if (result != 0)
                        {
                            return ServiceResult.Success;
                        }
                        return ServiceResult.Failed("خطایی در تراکنش رخ داده است");
                }
            }
            return ServiceResult.Failed("خطایی در تراکنش رخ داده است");
        }

        public async Task<int> CountByRequestAsync(ReceiptPaymentSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task<string> CreateAsync()
        {
            var receipt = await _receiptService.FindByUserIdAsync(_webContextManager.CurrentUserId, false);
            if (receipt == null)
                return null;

            var zarinpal = _paymentGatewayManager.ZarinpalGateway();
            string authorityCode;
            var status = zarinpal.PaymentRequest(_configurationManager.MerchantCode,
                Convert.ToInt32(receipt.TotalProductsPrice), _configurationManager.PaymentDescription, receipt.Email,
                receipt.PhoneNumber, _configurationManager.PaymentCallbackUrl, out authorityCode);
            switch (status)
            {
                case 100:
                    var payment = new ReceiptPayment
                    {
                        MerchantCode = _configurationManager.MerchantCode,
                        AuthorityCode = authorityCode,
                        Buy = BuyType.Product,
                        Pay = PayType.Zarinpal,
                        Amount = Convert.ToInt32(receipt.TotalProductsPrice),
                        Description = _configurationManager.PaymentDescription,
                        ReceiptId = receipt.Id,
                        MobileNumber = receipt.PhoneNumber,
                        Email = receipt.Email,
                        StatusCode = 100,
                        CreatedById = _webContextManager.CurrentUserId
                      
                    };
                    var resultAdd = _receiptPaymentRepository.Add(payment);
                    var result = await _unitOfWork.SaveAllChangesAsync();

                    if (result != 0)
                        return _configurationManager.ZarinpalGateway.PlusWord(authorityCode);
                    return null;

                default:
                    return null;
            }
        }

        public async Task<ReceiptPayment> FindByIdAsync(Guid paymentId)
        {
            return await _receiptPaymentRepository.FirstOrDefaultAsync(model => model.Id == paymentId);
        }

        public async Task<ReceiptPayment> FindByAuthorityCodeAsync(string authorityCode)
        {
            return await _receiptPaymentRepository.FirstOrDefaultAsync(model => model.AuthorityCode == authorityCode);
        }

        public async Task<IList<ReceiptPayment>> GetByRequestAsync(ReceiptPaymentSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public IQueryable<ReceiptPayment> QueryByRequest(ReceiptPaymentSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var payments = _receiptPaymentRepository.AsNoTracking().AsQueryable()
                .Include(m => m.Receipt);

            if (model.CreatedById != null)
                payments = payments.Where(m => m.CreatedById == model.CreatedById);
            if (model.AuthorityCode.HasValue())
                payments = payments.Where(m => m.AuthorityCode == model.AuthorityCode);

            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;

            payments = payments.OrderBy($"{model.SortMember} {model.SortDirection}");

            return payments;
        }

        #endregion Public Methods
    }
}