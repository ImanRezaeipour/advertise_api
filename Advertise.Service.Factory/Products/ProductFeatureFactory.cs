using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Products;
using Advertise.Service.Common;
using Advertise.Service.Products;
using AutoMapper;

namespace Advertise.Service.Factory.Products
{
    public class ProductFeatureFactory : IProductFeatureFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IProductFeatureService _productFeatureService;

        #endregion Private Fields

        #region Public Constructors

        public ProductFeatureFactory(ICommonService commonService, IMapper mapper, IProductFeatureService productFeatureService, IListService listService)
        {
            _commonService = commonService;
            _mapper = mapper;
            _productFeatureService = productFeatureService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ProductFeatureDetailModel> PrepareDetailModelAsync(Guid productFeatureId)
        {
            if (productFeatureId == null)
                throw new ArgumentNullException(nameof(productFeatureId));

            var productFeature = await _productFeatureService.FindByIdAsync(productFeatureId);
            var viewModel = _mapper.Map<ProductFeatureDetailModel>(productFeature);

            return viewModel;
        }

        public async Task<ProductFeatureEditModel> PrepareEditModelAsync(Guid productFeatureId)
        {
            if (productFeatureId == null)
                throw new ArgumentNullException(nameof(productFeatureId));

            var productFeature = await _productFeatureService.FindByIdAsync(productFeatureId);
            var viewModel = _mapper.Map<ProductFeatureEditModel>(productFeature);

            return viewModel;
        }

        public async Task<ProductFeatureListModel> PrepareListModelAsync(ProductFeatureSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            var viewModel = await _productFeatureService.ListByRequestAsync(model);

            viewModel.PageSizeList = await _listService.GetPageSizeListAsync();
            viewModel.SortDirectionList = await _listService.GetSortDirectionListAsync();

            return viewModel;
        }

        #endregion Public Methods
    }
}