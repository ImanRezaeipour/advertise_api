using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Products;
using Advertise.Service.Common;
using Advertise.Service.Products;

namespace Advertise.Service.Factory.Products
{
    public class ProductTagFactory : IProductTagFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IProductTagService _productTagService;

        #endregion Private Fields

        #region Public Constructors

        public ProductTagFactory(ICommonService commonService, IProductTagService productTagService, IListService listService)
        {
            _commonService = commonService;
            _productTagService = productTagService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ProductTagListModel> PrepareListModelAsync(ProductTagSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            var viewModel = await _productTagService.ListByRequestAsync(model);

            viewModel.PageSizeList = await _listService.GetPageSizeListAsync();
            viewModel.SortDirectionList = await _listService.GetSortDirectionListAsync();

            return viewModel;
        }

        #endregion Public Methods
    }
}