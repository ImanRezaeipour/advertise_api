using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web;
using Advertise.Core.Common;
using Advertise.Core.Domain.Announces;
using Advertise.Core.Exceptions;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.File;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Announces;
using Advertise.Core.Model.Common;
using Advertise.Core.Objects;
using Advertise.Data.DbContexts;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using AutoMapper;

namespace Advertise.Service.Announces
{
    public class AnnounceService : IAnnounceService
    {
        #region Private Fields

        private readonly IAnnounceOptionService _announceOptionService;
        private readonly IAnnouncePaymentService _announcePaymentService;
        private readonly IDbSet<Announce> _announceRepository;
        private readonly IAnnounceReserveService _announceReserveService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;
        private readonly ICompanyService _companyService;

        #endregion Private Fields

        #region Public Constructors

        public AnnounceService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IAnnounceOptionService adsOptionService, IAnnouncePaymentService adsPaymentService, IAnnounceReserveService adsReserveService, ICompanyService companyService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _announceOptionService = adsOptionService;
            _announcePaymentService = adsPaymentService;
            _announceReserveService = adsReserveService;
            _companyService = companyService;
            _announceRepository = unitOfWork.Set<Core.Domain.Announces.Announce>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task ApproveByIdAsync(Guid adsId)
        {
            var ads = await FindByIdAsync(adsId);
            if (ads == null)
                return;

            if (ads.IsApprove == true)
                return;

            ads.IsApprove = true;
            if (ads.DurationType != null)
            {
                var reserveViewModel = new AnnounceReserveCreateModel
                {
                    AdsId = adsId,
                    DurationType = ads.DurationType.Value,
                    AdsOptionId = ads.AnnounceOptionId
                };
                await _announceReserveService.CreateByViewModelAsync(reserveViewModel);
            }

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task<int> CountByRequestAsync(AnnounceSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).CountAsync();
        }

        public async Task<PaymentResult> CreateByViewModelAsync(AnnounceCreateModel viewModel, bool? isFreeOfCharge = null)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var ads = _mapper.Map<Core.Domain.Announces.Announce>(viewModel);
            if (viewModel.AdsOptionId != null)
            {
                var option = await _announceOptionService.FindByIdAsync(viewModel.AdsOptionId.Value);
                if (option == null)
                    throw new ServiceException();

                var finalPrice = viewModel.DurationType.ToInt32();
                ads.FinalPrice = option.Price * finalPrice;
            }
            ads.EntityName = "Product";
            if (ads.EntityId == null)
            {
                ads.EntityId = _companyService.CurrentCompanyId;
                ads.EntityName = "Company";
            }
            ads.TargetId = ads.EntityId;
            ads.OwnerId = _webContextManager.CurrentUserId;
            ads.IsApprove = isFreeOfCharge == true;
            _announceRepository.Add(ads);

            await _unitOfWork.SaveAllChangesAsync();

            if (isFreeOfCharge == true)
            {
                if (ads.DurationType != null)
                {
                    var reserveViewModel = new AnnounceReserveCreateModel
                    {
                        AdsId = ads.Id,
                        DurationType = ads.DurationType.Value,
                        AdsOptionId = ads.AnnounceOptionId
                    };
                    await _announceReserveService.CreateByViewModelAsync(reserveViewModel);
                }
                return PaymentResult.Success;
            }
            var zarinpalViewModel = new AnnouncePaymentCreateModel
            {
                Amount = ads.FinalPrice,
                AdsId = ads.Id,
            };
            return await _announcePaymentService.CreateWithZarinpalByViewModelAsync(zarinpalViewModel);
        }

        public async Task EditByViewModelAsync(AnnounceEditModel viewModel)
        {
            var ads = await FindByIdAsync(viewModel.Id);
            if (ads == null)
                throw new ServiceException();

            _mapper.Map(viewModel, ads);
            ads.IsApprove = true;

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task<Core.Domain.Announces.Announce> FindByIdAsync(Guid bannerId)
        {
            return await _announceRepository.FirstOrDefaultAsync(model => model.Id == bannerId);
        }

        public async Task<IList<Announce>> GetByRequestAsync(AnnounceSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).ToPagedListAsync(request.PageIndex, request.PageSize);
        }

        public async Task<IList<FineUploaderObject>> GetFileAsFineUploaderModelByIdAsync(Guid categoryId)
        {
            return (await _announceRepository.AsNoTracking()
                    .Where(model => model.Id == categoryId)
                    .Select(model => new
                    {
                        model.Id,
                        model.ImageFileName
                    }).ToListAsync())
                .Select(model => new FineUploaderObject
                {
                    id = model.Id.ToString(),
                    uuid = model.Id.ToString(),
                    name = model.ImageFileName,
                    thumbnailUrl = FileConst.ImagesWebPath.PlusWord(model.ImageFileName),
                    size = new FileInfo(HttpContext.Current.Server.MapPath(FileConst.ImagesWebPath.PlusWord(model.ImageFileName))).Length.ToString()
                }).ToList();
        }

        public IQueryable<Core.Domain.Announces.Announce> QueryByRequest(AnnounceSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var adses = _announceRepository.AsNoTracking().AsQueryable()
                .Include(model => model.AnnounceOption);

            if (request.OwnerId.HasValue)
                adses = adses.Where(model => model.OwnerId == request.OwnerId);
            if (request.IsApprove.HasValue)
                adses = adses.Where(model => model.IsApprove == request.IsApprove);

            if (string.IsNullOrEmpty(request.SortMember))
                request.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(request.SortDirection))
                request.SortDirection = SortDirection.Desc;

            adses = adses.OrderBy($"{request.SortMember} {request.SortDirection}");

            return adses;
        }

        public async Task RejectByIdAsync(Guid adsId)
        {
            var ads = await FindByIdAsync(adsId);
            if (ads == null)
                return;

            ads.IsApprove = false;

            await _unitOfWork.SaveAllChangesAsync();
        }

        #endregion Public Methods
    }
}