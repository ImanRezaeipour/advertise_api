using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Products;
using Advertise.Service.Common;
using Advertise.Service.Products;
using AutoMapper;

namespace Advertise.Service.Factory.Products
{
    public class ProductImageFactory : IProductImageFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IProductImageService _productImageService;

        #endregion Private Fields

        #region Public Constructors

        public ProductImageFactory(ICommonService commonService, IMapper mapper, IProductImageService productImageService, IListService listService)
        {
            _commonService = commonService;
            _mapper = mapper;
            _productImageService = productImageService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ProductImageDetailModel> PrepareDetailModelAsync(Guid productImageId)
        {
            if (productImageId == null)
                throw new ArgumentNullException(nameof(productImageId));

            var productImage = await _productImageService.FindByIdAsync(productImageId);
            var viewModel = _mapper.Map<ProductImageDetailModel>(productImage);

            return viewModel;
        }

        public async Task<ProductImageEditModel> PrepareEditModelAsync(Guid productImageId)
        {
            if (productImageId == null)
                throw new ArgumentNullException(nameof(productImageId));

            var productImage = await _productImageService.FindByIdAsync(productImageId);
            var viewModel = _mapper.Map<ProductImageEditModel>(productImage);

            return viewModel;
        }

        public async Task<ProductImageListModel> PrepareListModelAsync(ProductImageSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            var viewModel = await _productImageService.ListByRequestAsync(model);

            viewModel.PageSizeList = await _listService.GetPageSizeListAsync();
            viewModel.SortDirectionList = await _listService.GetSortDirectionListAsync();

            return viewModel;
        }

        #endregion Public Methods
    }
}