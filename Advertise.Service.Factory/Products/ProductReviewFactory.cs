using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Helpers;
using Advertise.Core.Model.Products;
using Advertise.Core.Types;
using Advertise.Service.Common;
using Advertise.Service.Products;
using AutoMapper;

namespace Advertise.Service.Factory.Products
{
    public class ProductReviewFactory : IProductReviewFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IProductReviewService _productReviewService;
        private readonly IProductService _productService;

        #endregion Private Fields

        #region Public Constructors

        public ProductReviewFactory(ICommonService commonService, IProductReviewService productReviewService, IMapper mapper, IProductService productService, IListService listService)
        {
            _commonService = commonService;
            _productReviewService = productReviewService;
            _mapper = mapper;
            _productService = productService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ProductReviewDetailModel> PrepareDetailModelAsync(Guid productReviewId)
        {
            var productReview = await _productReviewService.FindByIdAsync(productReviewId);
            var viewModel = _mapper.Map<ProductReviewDetailModel>(productReview);

            return viewModel;
        }

        public async Task<ProductReviewCreateModel> PrepareCreateModelAsync(ProductReviewCreateModel modelPrepare= null)
        {
            var viewModel = modelPrepare?? new ProductReviewCreateModel();

            viewModel.ProductList = await _productService.GetAsSelectListAsync();
            return viewModel;

        }

        public async Task<ProductReviewEditModel> PrepareEditModelAsync(Guid productReviewId)
        {
            var productReview = await _productReviewService.FindByIdAsync(productReviewId);
            var viewModel = _mapper.Map<ProductReviewEditModel>(productReview);

            return viewModel;
        }

        public async Task<ProductReviewListModel> PrepareListModelAsync(ProductReviewSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _productReviewService.CountByRequestAsync(model);
            var productReviews = await _productReviewService.GetByRequestAsync(model);
            var productReviewViewModel = _mapper.Map<IList<ProductReviewModel>>(productReviews);
            var viewModel = new ProductReviewListModel
            {
                SearchModel = model,
                ProductReviews = productReviewViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                StateList = EnumHelper.CastToSelectListItems<StateType>(),
                ActiveList = EnumHelper.CastToSelectListItems<ActiveType>()
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}