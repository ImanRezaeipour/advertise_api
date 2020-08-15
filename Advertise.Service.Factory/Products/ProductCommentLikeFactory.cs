using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Products;
using Advertise.Service.Common;
using Advertise.Service.Products;
using AutoMapper;

namespace Advertise.Service.Factory.Products
{
    public class ProductCommentLikeFactory : IProductCommentLikeFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IProductCommentLikeService _productCommentLikeService;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public ProductCommentLikeFactory(ICommonService commonService, IProductCommentLikeService productCommentLikeService, IMapper mapper, IListService listService)
        {
            _commonService = commonService;
            _productCommentLikeService = productCommentLikeService;
            _mapper = mapper;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ProductCommentLikeListModel> PrepareListViewModelAsync(ProductCommentLikeSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.LikedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _productCommentLikeService.CountByRequestAsync(model);
            var productComments = await _productCommentLikeService.GetByRequestAsync(model);
            var productCommentViewModel = _mapper.Map<IList<ProductCommentLikeModel>>(productComments);
            var viewModel = new ProductCommentLikeListModel
            {
                SearchModel = model,
                ProductComments = productCommentViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync()
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}