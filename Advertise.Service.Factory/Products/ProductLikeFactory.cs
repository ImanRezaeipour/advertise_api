using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Products;
using Advertise.Service.Common;
using Advertise.Service.Products;
using AutoMapper;

namespace Advertise.Service.Factory.Products
{
    public class ProductLikeFactory : IProductLikeFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IProductLikeService _productLikeService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public ProductLikeFactory( ICommonService commonService, IProductLikeService productLikeService, IMapper mapper, IListService listService)
        {
            _commonService = commonService;
            _productLikeService = productLikeService;
            _mapper = mapper;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ProductLikeListModel> PrepareListModelAsync(ProductLikeSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.LikedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _productLikeService.CountByRequestAsync(model);
            var productLikes = await _productLikeService.GetByRequestAsync(model);
            var productLikeViewModel = _mapper.Map<IList<ProductLikeModel>>(productLikes);
            var listViewModel = new ProductLikeListModel
            {
                SearchModel = model,
                ProductLikes = productLikeViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
            };

            return listViewModel;
        }

        public async Task<ProductLikeListModel> PrepareLikerListModelAsync(ProductLikeSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.LikedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _productLikeService.CountByRequestAsync(model);
            var productLikes = await _productLikeService.GetByRequestAsync(model);
            var productLikeViewModel = _mapper.Map<IList<ProductLikeModel>>(productLikes);
            var listViewModel = new ProductLikeListModel
            {
                SearchModel = model,
                ProductLikes = productLikeViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
            };

            return listViewModel;
        }

        #endregion Public Methods
    }
}