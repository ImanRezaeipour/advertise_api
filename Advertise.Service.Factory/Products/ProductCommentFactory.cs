using System;
using System.Threading.Tasks;
using Advertise.Core.Helpers;
using Advertise.Core.Model.Products;
using Advertise.Core.Types;
using Advertise.Service.Common;
using Advertise.Service.Products;
using AutoMapper;

namespace Advertise.Service.Factory.Products
{
    public class ProductCommentFactory : IProductCommentFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IProductCommentService _productCommentService;

        #endregion Private Fields

        #region Public Constructors

        public ProductCommentFactory(ICommonService commonService, IMapper mapper, IProductCommentService productCommentService, IListService listService)
        {
            _commonService = commonService;
            _mapper = mapper;
            _productCommentService = productCommentService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ProductCommentDetailModel> PrepareDetailModelAsync(Guid productCommentId)
        {
            if (productCommentId == Guid.Empty)
                throw new ArgumentNullException(nameof(productCommentId));

            var productComment = await _productCommentService.FindByIdAsync(productCommentId);
            var viewModel = _mapper.Map<ProductCommentDetailModel>(productComment);

            return viewModel;
        }

        public async Task<ProductCommentEditModel> PrepareEditModelAsync(Guid productCommentId, bool applyCurrentUser = false)
        {
            if (productCommentId == Guid.Empty)
                throw new ArgumentNullException(nameof(productCommentId));

            var productComment = await _productCommentService.FindByIdAsync(productCommentId);
            var viewModel = _mapper.Map<ProductCommentEditModel>(productComment);

            if (applyCurrentUser)
                viewModel.IsMine = true;

            return viewModel;
        }

        public async Task<ProductCommentListModel> PrepareListModelAsync(ProductCommentSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CommentedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            var viewModel = await _productCommentService.ListByRequestAsync(model);

            viewModel.PageSizeList = await _listService.GetPageSizeListAsync();
            viewModel.SortDirectionList = await _listService.GetSortDirectionListAsync();
            viewModel.StateTypeList = EnumHelper.CastToSelectListItems<StateType>();

            if (isCurrentUser)
                viewModel.IsMine = true;

            return viewModel;
        }

        #endregion Public Methods
    }
}