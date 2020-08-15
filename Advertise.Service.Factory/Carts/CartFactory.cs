using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Carts;
using Advertise.Core.Model.Locations;
using Advertise.Service.Carts;
using Advertise.Service.Common;
using Advertise.Service.Locations;
using Advertise.Service.Users;
using AutoMapper;

namespace Advertise.Service.Factory.Carts
{
    public class CartFactory : ICartFactory
    {
        #region Private Fields

        private readonly ILocationService _addressService;
        private readonly ICartService _bagService;
        private readonly ICommonService _commonService;
        private readonly IMapper _mapper;
        private readonly IWebContextManager _webContextManager;
        private readonly IUserService _userService;

        #endregion Private Fields

        #region Public Constructors

        public CartFactory(ICommonService commonService, ILocationService addressService, ICartService bagService, IWebContextManager webContextManager, IMapper mapper, IUserService userService)
        {
            _commonService = commonService;
            _addressService = addressService;
            _bagService = bagService;
            _webContextManager = webContextManager;
            _mapper = mapper;
            _userService = userService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CartDetailModel> PrepareDetailModelAsync()
        {
            var viewModel = new CartDetailModel();

            var request = new CartSearchModel
            {
                SortDirection = SortDirection.Desc,
                SortMember = SortMember.CreatedOn,
                CreatedById = _webContextManager.CurrentUserId,
                PageSize = PageSize.Count100,
                PageIndex = 1
            };
            var bags = await _bagService.GetByRequestAsync(request);

            viewModel.Carts = _mapper.Map<List<CartModel>>(bags);

            return viewModel;
        }

        public async Task<CartListModel> PrepareListModelAsync(CartSearchModel request, bool isCurrentUser = false, Guid? userId = null)
        {
            request.PageSize = PageSize.All;
            request.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            var bags = await _bagService.GetByRequestAsync(request);
            request.TotalCount = bags.Count;
            var bagsViewModel = _mapper.Map<IList<CartModel>>(bags);
            var bagsList = new CartListModel
            {
                SearchRequest = request,
                Carts = bagsViewModel
            };

            return bagsList;
        }

        public async Task<CartInfoModel> PrepareInfoModelAsync()
        {
            var bag = await _bagService.FindByUserIdAsync(_webContextManager.CurrentUserId);
            var viewModel = _mapper.Map<CartInfoModel>(bag);

            var address = await _userService.GetAddressByIdAsync(_webContextManager.CurrentUserId);
            viewModel.Location = _mapper.Map<LocationModel>(address);
            viewModel.AddressProvince = await _addressService.GetProvinceAsSelectListItemAsync();

            return viewModel;
        }

        #endregion Public Methods
    }
}