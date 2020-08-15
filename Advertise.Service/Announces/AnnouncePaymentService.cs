using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Configuration;
using Advertise.Core.Domain.Announces;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.PaymentGateway;
using Advertise.Core.Managers.Security;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Announces;
using Advertise.Core.Model.Common;
using Advertise.Data.DbContexts;
using Advertise.Data.Validation.Common;
using Advertise.Service.Common;
using Advertise.Service.Users;
using AutoMapper;

namespace Advertise.Service.Announces
{
    public class AnnouncePaymentService : IAnnouncePaymentService
    {
        #region Private Fields

        private readonly IDbSet<AnnouncePayment> _announcePaymentRepository;
        private readonly IConfigurationManager _configurationManager;
        private readonly IZarinpalGatewayManager _gatewayManager;
        private readonly IJwtManager _jwtManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IWebContextManager _webContextManager;
        private readonly IModelValidator _modelValidator;

        #endregion Private Fields

        #region Public Constructors

        public AnnouncePaymentService(IUserService userService, IZarinpalGatewayManager gatewayManager, IJwtManager jwtManager, IConfigurationManager configurationManager, IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IModelValidator modelValidator)
        {
            _userService = userService;
            _gatewayManager = gatewayManager;
            _jwtManager = jwtManager;
            _configurationManager = configurationManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _modelValidator = modelValidator;
            _announcePaymentRepository = unitOfWork.Set<AnnouncePayment>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<PaymentResult> CallbackWithZarinpalByViewModelAsync(AnnouncePaymentCallbackModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.Status))
                throw new ArgumentNullException(nameof(viewModel.Status));

            if (string.IsNullOrEmpty(viewModel.Authority))
                throw new ArgumentNullException(nameof(viewModel.Authority));

            var adsPayment = await FindByAuthorityCodeAsync(viewModel.Authority);
            if (adsPayment == null)
                return PaymentResult.Failed();

            var zarinpal = _gatewayManager.ZarinpalGateway();
            long referenceCode;
            var status = zarinpal.PaymentVerification(_configurationManager.MerchantCode, adsPayment.AuthorityCode, (int)adsPayment.Amount.GetValueOrDefault(), out referenceCode);

            adsPayment.ReferenceCode = (int?)referenceCode;
            adsPayment.StatusCode = status;
            adsPayment.IsComplete = true;

            switch (status)
            {
                case 100:
                case 101:
                    adsPayment.IsSuccess = true;
                    await _unitOfWork.SaveAllChangesAsync();
                    return PaymentResult.Success;

                // case -1: // اطلاعات ارسال شده ناقص است
                // case -2: // IP و يا مرچنت كد پذيرنده صحيح نيست
                // case -3: // با توجه به محدوديت هاي شاپرك امكان پرداخت با رقم درخواست شده ميسر نمي باشد
                // case -4: // سطح تاييد پذيرنده پايين تر از سطح نقره اي است
                // case -11: // درخواست مورد نظر يافت نشد
                // case -12: // امكان ويرايش درخواست ميسر نمي باشد
                // case -21: // هيچ نوع عمليات مالي براي اين تراكنش يافت نشد
                // case -22: // تراكنش نا موفق ميباشد
                // case -33: // رقم تراكنش با رقم پرداخت شده مطابقت ندارد.
                // case -34: // سقف تقسيم تراكنش از لحاظ تعداد يا رقم عبور نموده است
                // case -40: // اجازه دسترسي به متد مربوطه وجود ندارد
                // case -41: // اطلاعات ارسال شده مربوط به  AdditionalDataغيرمعتبر ميباشد
                // case -42: // مدت زمان معتبر طول عمر شناسه پرداخت بايد بين  30دقيه تا  45روز مي باشد
                // case -54: // درخواست مورد نظر آرشيو شده است

                default:
                    adsPayment.IsSuccess = false;
                    await _unitOfWork.SaveAllChangesAsync(auditUserId: _webContextManager.CurrentUserId);
                    return PaymentResult.Failed();
            }
        }

        public async Task<int> CountByRequestAsync(AnnouncePaymentSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException();

            return await QueryByRequest(request).CountAsync();
        }

        public async Task<PaymentResult> CreateWithZarinpalByViewModelAsync(AnnouncePaymentCreateModel viewModel)
        {
            var user = await _userService.FindByUserIdAsync(Guid.Empty, true);

            if (user == null)
                return PaymentResult.Failed();

            var zarinpal = _gatewayManager.ZarinpalGateway();
            string authorityCode;
            var status = zarinpal.PaymentRequest(_configurationManager.MerchantCode, Convert.ToInt32(viewModel.Amount), _configurationManager.PaymentDescription, user.Email, user.Meta.PhoneNumber, _configurationManager.AdsPaymentCallbackUrl, out authorityCode);

            var adsPayment = new AnnouncePayment
            {
                AnnounceId = viewModel.AdsId,
                AuthorityCode = authorityCode,
                StatusCode = status,
                Amount = viewModel.Amount,
                CreatedById = _webContextManager.CurrentUserId,
                CreatedOn = DateTime.Now
            };
            _announcePaymentRepository.Add(adsPayment);

            await _unitOfWork.SaveAllChangesAsync();

            switch (status)
            {
                case 100:
                    return PaymentResult.Succeed(_configurationManager.ZarinpalGateway.PlusWord(authorityCode));

                default:
                    return PaymentResult.Failed();
            }
        }

        public async Task<AnnouncePayment> FindByAuthorityCodeAsync(string authorityCode)
        {
            return await _announcePaymentRepository.FirstOrDefaultAsync(model => model.AuthorityCode == authorityCode);
        }

        public async Task<IList<AnnouncePayment>> GetByRequestAsync(AnnouncePaymentSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).ToPagedListAsync(request.PageIndex, request.PageSize);
        }

        public IQueryable<AnnouncePayment> QueryByRequest(AnnouncePaymentSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var adsPayment = _announcePaymentRepository.AsNoTracking().AsQueryable();
            if (request.Term.HasValue())
                adsPayment = adsPayment.Where(model => model.AuthorityCode.Contains(request.Term));
            if (request.IsApprove.HasValue)
                adsPayment = adsPayment.Where(model => model.Announce.IsApprove.Value == request.IsApprove);
            if (request.CreatedById.HasValue)
                adsPayment = adsPayment.Where(model => model.CreatedById == request.CreatedById);

            if (string.IsNullOrEmpty(request.SortDirection))
                request.SortDirection = SortDirection.Desc;
            if (string.IsNullOrEmpty(request.SortMember))
                request.SortMember = SortMember.CreatedOn;

            adsPayment = adsPayment.OrderBy($"{request.SortMember} {request.SortDirection}");

            return adsPayment;
        }

        #endregion Public Methods
    }
}