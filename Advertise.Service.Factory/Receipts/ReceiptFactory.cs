using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Locations;
using Advertise.Core.Model.Receipts;
using Advertise.Service.Common;
using Advertise.Service.Locations;
using Advertise.Service.Receipts;
using Advertise.Service.Users;
using AutoMapper;

namespace Advertise.Service.Factory.Receipts
{
    public class ReceiptFactory : IReceiptFactory
    {
        #region Private Fields

        private readonly ILocationService _addressService;
        private readonly ILocationCityService _cityService;
        private readonly ICommonService _commonService;
        private readonly IListService _listManager;
        private readonly IMapper _mapper;
        private readonly IReceiptOptionService _receiptOptionService;
        private readonly IReceiptService _receiptService;
        private readonly IUserService _userService;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public ReceiptFactory(IListService listManager, ICommonService commonService, IMapper mapper, IWebContextManager webContextManager, ILocationService addressService, IReceiptOptionService receiptOptionService, IReceiptService receiptService, IUserService userService, ILocationCityService cityService)
        {
            _listManager = listManager;
            _commonService = commonService;
            _mapper = mapper;
            _webContextManager = webContextManager;
            _addressService = addressService;
            _receiptOptionService = receiptOptionService;
            _receiptService = receiptService;
            _userService = userService;
            _cityService = cityService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ReceiptDetailModel> PrepareDetailModelAsync(Guid receiptId)
        {
            if (receiptId == null)
                throw new ArgumentNullException(nameof(receiptId));

            var receipt = _receiptService.FindByIdAsync(receiptId);
            var viewModel = _mapper.Map<ReceiptDetailModel>(receipt);

            return viewModel;
        }

        public async Task<ReceiptModel> PrepareCreateModelAsync(ReceiptModel modelPrepare = null)
        {
            var viewModel = modelPrepare?? new ReceiptModel();
            var user = await _userService.FindByIdAsync(_webContextManager.CurrentUserId);
            var userMeta = user.Meta;
            var receipt = await _receiptService.FindByUserIdAsync(_webContextManager.CurrentUserId);
            var receiptLastAddress = await _receiptService.FindLastAddressByUserIdAsync(_webContextManager.CurrentUserId);
            if (receipt != null)
            {
                if (receipt.Location != null)
                {
                    viewModel.Location = _mapper.Map<LocationCreateModel>(receipt.Location);
                    viewModel.Location.Street = receipt.Location.Street;
                    viewModel.Location.Extra = receipt.Location.Extra;
                    viewModel.Location.PostalCode = receipt.Location.PostalCode;
                }
            }
           
            else if (receiptLastAddress != null)
            {
                if (receiptLastAddress.Location != null)
                {
                    viewModel.Location = _mapper.Map<LocationCreateModel>(receiptLastAddress.Location);
                    viewModel.Location.Street = receiptLastAddress.Location.Street;
                    viewModel.Location.Extra = receiptLastAddress.Location.Extra;
                    viewModel.Location.PostalCode = receiptLastAddress.Location.PostalCode;
                }
            }
            else if (userMeta.Location != null)
            {
                viewModel.Location = _mapper.Map<LocationCreateModel>(userMeta.Location);
                viewModel.Location.Street = userMeta.Location.Street;
                viewModel.Location.Extra = userMeta.Location.Extra;
                viewModel.Location.PostalCode = userMeta.Location.PostalCode;
            }
            else
            {
                viewModel.Location = new LocationCreateModel();
            }

            if (viewModel.Location.LocationCity == null)
                viewModel.Location.LocationCity = new LocationCityModel();

            viewModel.AddressProvince = await _addressService.GetProvinceAsSelectListItemAsync();
            viewModel.Email = user.Email;
            viewModel.PhoneNumber = userMeta.PhoneNumber;
            viewModel.FirstName = userMeta.FirstName;
            viewModel.LastName = userMeta.LastName;
            viewModel.HomeNumber = userMeta.HomeNumber;
            viewModel.NationalCode = userMeta.NationalCode;
            viewModel.TransfereeName = userMeta.FullName;
            return viewModel;
        }

        public async Task<ReceiptEditModel> PrepareEditModelAsync(Guid receiptId)
        {
            var receipt = await _receiptService.FindByIdAsync(receiptId);
            var viewModel = _mapper.Map<ReceiptEditModel>(receipt);

            return viewModel;
        }

        public async Task<ReceiptListModel> PrepareListModelAsync(ReceiptSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            //var viewModel = await _receiptService.ListByRequestAsync(request);

            model.TotalCount = await _receiptService.CountByRequestAsync(model);
            var receipts = await _receiptService.GetByRequestAsync(model);
            var map = _mapper.Map<List<ReceiptModel>>(receipts);
            var viewModel = new ReceiptListModel
            {
                Receipts = map,
                SearchModel = model,
                PageSizeList = await _listManager.GetPageSizeListAsync(),
                SortDirectionList = await _listManager.GetSortDirectionListAsync(),
            };

            if (isCurrentUser)
                viewModel.IsMine = true;

            return viewModel;
        }

        public async Task<ReceiptPreviewModel> PreparePreviewModelAsync(Guid? id= null)
        {
            object receipt = null;
            if (id != null)
                receipt = await _receiptService.FindByIdAsync(id.GetValueOrDefault());
             receipt = await _receiptService.FindByUserIdAsync(_webContextManager.CurrentUserId, false);

            var viewModel = _mapper.Map<ReceiptPreviewModel>(receipt);

            var address = await _receiptService.GetAddressViewModelAsync(viewModel.Id);
            if (address != null)
            {
                viewModel.Location = _mapper.Map<LocationModel>(address);
                var province = await _cityService.FindByIdAsync(address.LocationCity.ParentId);
                viewModel.Location.LocationCity.Name = address.LocationCity.Name;
                viewModel.ProvinceName = province.Name;
                if (viewModel.Location.LocationCity == null)
                    viewModel.Location.LocationCity = new LocationCityModel();
            }
            else
            {
                viewModel.Location = new LocationModel { LocationCity = new LocationCityModel() };
            }

            var receiptOptions = await _receiptOptionService.GetMyReceiptOptionsByReceiptIdAsync(viewModel.Id);
            viewModel.ReceiptOptions = receiptOptions.Select(model => new ReceiptOptionModel
            {
                TotalPrice = model.TotalPrice,
                CategoryTitle = model.CategoryTitle,
                Title = model.Title,
                CompanyTitle = model.CompanyTitle,
                PreviousPrice = model.PreviousPrice,
                Count = model.Count,
                Price = model.Price,
            }).ToList();

            return viewModel;
        }


        public async Task<ReceiptModel> PrepareModelAsync()
        {
            var viewModel = new ReceiptModel();
            return viewModel;
        }

        #endregion Public Methods
    }
}