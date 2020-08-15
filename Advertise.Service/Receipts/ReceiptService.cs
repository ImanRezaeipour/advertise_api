using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Locations;
using Advertise.Core.Domain.Receipts;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Locations;
using Advertise.Core.Model.Receipts;
using Advertise.Data.DbContexts;
using Advertise.Service.Carts;
using Advertise.Service.Common;
using Advertise.Service.Locations;
using AutoMapper;
using Z.EntityFramework.Plus;

namespace Advertise.Service.Receipts
{
    public class ReceiptService : IReceiptService
    {
        #region Private Fields

        private readonly ILocationService _addressService;
        private readonly ICartService _bagService;
        private readonly IMapper _mapper;
        private readonly IDbSet<ReceiptOption> _receiptOptionRepository;
        private readonly IDbSet<Receipt> _receiptRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public ReceiptService(IMapper mapper, ICartService bagService, IUnitOfWork unitOfWork, IWebContextManager webContextManager, ILocationService addressService)
        {
            _mapper = mapper;
            _bagService = bagService;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _addressService = addressService;
            _receiptRepository = unitOfWork.Set<Receipt>();
            _receiptOptionRepository = unitOfWork.Set<ReceiptOption>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<bool> HasByCurrentUserAsync()
        {
          return  await _receiptRepository.AnyAsync(model => model.CreatedById == _webContextManager.CurrentUserId);
            
        }

        public async Task<int> CountByRequestAsync(ReceiptSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

          return  await QueryByRequest(model).CountAsync();
            
        }

        public async Task CreateByViewModel(ReceiptModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var receipt = _mapper.Map<Receipt>(model);

            var bags = await _bagService.GetByUserIdAsync(_webContextManager.CurrentUserId);
            var receiptOptions = bags.Select(m => new ReceiptOption
            {
                CategoryCode = m.Product.Category.Code,
                CategoryTitle = m.Product.Category.Title,
                Code = m.Product.Code,
                CompanyCode = m.Product.Company.Code,
                Count = m.ProductCount,
                CompanyTitle = m.Product.Company.Title,
                PreviousPrice = m.Product.PreviousPrice,
                DiscountPercent = m.Product.DiscountPercent,
                Price = m.Product.Price,
                Title = m.Product.Title,
                TotalPrice = m.ProductCount * m.Product.Price,
                SaledById = m.Product.CreatedById
            }).ToList();

            receipt.Options = receiptOptions;
            receipt.TotalProductsPrice = receiptOptions.Sum(m => m.TotalPrice);

            var address = _mapper.Map<Location>(model.Location);
            if (address == null)
            {
                receipt.Location = null;
            }
            else
            {
                address.LocationCity = null;
                address.CityId = model.Location.LocationCity.Id;
                receipt.Location = address;
            }

            receipt.CreatedById = _webContextManager.CurrentUserId;
            receipt.IsBuy = false;
            receipt.CreatedById = _webContextManager.CurrentUserId;
            receipt.CreatedOn =DateTime.Now;
            _receiptRepository.Add(receipt);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid receiptId)
        {
            var receipt = await _receiptRepository.FirstOrDefaultAsync(model => model.Id == receiptId);
            receipt?.Options.Clear();
            _receiptRepository.Remove(receipt);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task EditAddressByIdAsync(Guid receiptId, Location address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            var userMeta = await _receiptRepository.FirstOrDefaultAsync(model => model.Id == receiptId);
            if (userMeta != null)
            {
                userMeta.Location.Latitude = address.Latitude;
                userMeta.Location.Longitude = address.Longitude;
                userMeta.Location.Extra = address.Extra;
                userMeta.Location.PostalCode = address.PostalCode;
                userMeta.Location.Street = address.Street;
                userMeta.Location.CityId = address.CityId;
            }

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task EditByViewModelAsync(ReceiptModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var address = _mapper.Map<Location>(model.Location);

            var receiptOrginal = await _receiptRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            if (address == null)
            {
                model.Location = new LocationCreateModel { LocationCity = new LocationCityModel() };
            }
            else
            {
                model.Location = _mapper.Map<LocationCreateModel>(address);
                if (model.Location.LocationCity == null)
                    model.Location.LocationCity = new LocationCityModel();
            }
            await EditAddressByIdAsync(receiptOrginal.Id, address);

            var receiptOptionList = _mapper.Map<List<ReceiptOption>>(receiptOrginal.Options);
            if (receiptOptionList == null)
            {
                _receiptOptionRepository.Where(m => m.ReceiptId == model.Id).Delete();
            }
            else
            {
                _receiptOptionRepository.Where(m => m.ReceiptId == model.Id).Delete();
                receiptOrginal.Options.AddRange(receiptOptionList.ToArray());
            }
             _mapper.Map(model, receiptOrginal);
            receiptOrginal.IsBuy = false;
            
            var bags = await _bagService.GetByUserIdAsync(_webContextManager.CurrentUserId);
            var receiptOptions = bags.Select(m => new ReceiptOption
            {
                CategoryCode = m.Product.Category.Code,
                CategoryTitle = m.Product.Category.Title,
                Code = m.Product.Code,
                CompanyCode = m.Product.Company.Code,
                Count = m.ProductCount,
                CompanyTitle = m.Product.Company.Title,
                PreviousPrice = m.Product.PreviousPrice,
                DiscountPercent = m.Product.DiscountPercent,
                Price = m.Product.Price,
                Title = m.Product.Title,
                TotalPrice = m.ProductCount * m.Product.Price,
            })
                .ToList();
            receiptOrginal.TotalProductsPrice = receiptOptions.Sum(m => m.TotalPrice);

            foreach (var option in receiptOptions)
                receiptOrginal?.Options.Add(option);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task FinalUpdateByViewModel(ReceiptModel model)
        {
            var exist = await IsExistByUserIdAsync(_webContextManager.CurrentUserId, true);
            var notExist = await IsExistByUserIdAsync(_webContextManager.CurrentUserId, false);
            if ( notExist)
            {
                var reciept = await _receiptRepository.FirstOrDefaultAsync(m =>
                    m.CreatedById == _webContextManager.CurrentUserId && m.IsBuy == false);
                model.Id = reciept.Id;
                await EditByViewModelAsync(model);
            }
            else
                 await CreateByViewModel(model);
        }

        public async Task<Receipt> FindByIdAsync(Guid receiptId)
        {
            return  await _receiptRepository.FirstOrDefaultAsync(model => model.Id == receiptId);
        }

        public async Task<Receipt> FindByUserIdAsync(Guid userId, bool? isBuy = null)
        {
            var query = _receiptRepository.AsQueryable().Where(model => model.CreatedById == userId);

            if (isBuy.HasValue)
                query = query.Where(model => model.IsBuy == isBuy.Value);
            return  await query.FirstOrDefaultAsync();
        }

        public async Task<Receipt> FindLastAddressByUserIdAsync(Guid userId)
        {
            return  await _receiptRepository
                .Where(model => model.CreatedById == userId && model.IsBuy == true)
                .OrderByDescending(model => model.CreatedOn)
                .FirstOrDefaultAsync();
        }

        public async Task<string> GenerateCodeForReceiptAsync()
        {
            var request = new ReceiptSearchModel
            {
                PageSize = PageSize.All
            };
            var maxCode = await MaxCodeByRequestAsync(request, ReceiptAggregateMemberModel.InvoiceNumber);

            if (maxCode == null)
                return (CodeConst.BeginNumber5Digit);
           return  maxCode.ExtractNumeric();
        }

        public async Task<Location> GetAddressByUserId(Guid userId)
        {
            var receipt = await _receiptRepository
                .Include(model => model.CreatedBy.Meta.Location)
                .FirstOrDefaultAsync(model => model.CreatedById == userId);

            return  receipt?.CreatedBy?.Meta?.Location;
        }

        public async Task<LocationModel> GetAddressViewModelAsync(Guid receiptId)
        {
            var receipt = await FindByIdAsync(receiptId);
            var address = await _addressService.FindByIdAsync(receipt.LocationId.GetValueOrDefault()) ?? new Location();
           return  _mapper.Map<LocationModel>(address);
        }

        public async Task<IList<Receipt>> GetByRequestAsync(ReceiptSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

           return  await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<bool> IsExistByUserIdAsync(Guid userId, bool? isBuy = null)
        {
            var query = _receiptRepository.AsQueryable().Where(model => model.CreatedById == userId);
            if (isBuy.HasValue)
                query = query.Where(model => model.IsBuy == isBuy.Value);
          return  await query.AnyAsync();
        }

        public async Task<string> MaxCodeByRequestAsync(ReceiptSearchModel model, string aggregateMember)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var products = QueryByRequest(model);
            switch (aggregateMember)
            {
                case "InvoiceNumber":
                    var memberMax = await products.MaxAsync(m => m.InvoiceNumber);
                    return memberMax;
            }

            return null;
        }

        public IQueryable<Receipt> QueryByRequest(ReceiptSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var receipts = _receiptRepository.AsNoTracking().AsQueryable();
            if (model.CreatedById.HasValue)
                receipts = receipts.Where(m => m.CreatedById == model.CreatedById);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            receipts = receipts.OrderBy($"{model.SortMember} {model.SortDirection}");

            return receipts;
        }

        public async Task SetInvoiceNumberAsync(Guid receiptId, string invoiceNumber)
        {
            var receipt = await _receiptRepository.FirstOrDefaultAsync(model => model.Id == receiptId);
            receipt.InvoiceNumber = invoiceNumber;

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task SetIsBuyByReceiptIdAsync(Guid receiptId, bool isBuy)
        {
            var receipt = await _receiptRepository.FirstOrDefaultAsync(model => model.Id == receiptId);
            receipt.IsBuy = isBuy;

            await _unitOfWork.SaveAllChangesAsync();
        }

        #endregion Public Methods
    }
}