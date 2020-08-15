using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Helpers;
using Advertise.Core.Model.Categories;
using Advertise.Core.Types;
using Advertise.Service.Categories;
using Advertise.Service.Common;
using AutoMapper;

namespace Advertise.Service.Factory.Categories
{
    public class CategoryReviewFactory : ICategoryReviewFactory
    {
        #region Private Fields

        private readonly ICategoryReviewService _categoryReviewService;
        private readonly ICommonService _commonService;
        private readonly IMapper _mapper;
        private readonly IListService _listService;

        #endregion Private Fields

        #region Public Constructors

        public CategoryReviewFactory(ICategoryReviewService categoryReviewService, IMapper mapper, ICommonService commonService, IListService listService)
        {
            _categoryReviewService = categoryReviewService;
            _mapper = mapper;
            _commonService = commonService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CategoryReviewDetailModel> PrepareDetailModelAsync(Guid categoryReviewId)
        {
            var catgoryReview = await _categoryReviewService.FindByIdAsync(categoryReviewId);
            var viewModel = _mapper.Map<CategoryReviewDetailModel>(catgoryReview);

            return viewModel;
        }

        public async Task<CategoryReviewEditModel> PrepareEditModelAsync(Guid categoryReviewId)
        {
            var categoryReview = await _categoryReviewService.FindByIdAsync(categoryReviewId);
            var viewModel = _mapper.Map<CategoryReviewEditModel>(categoryReview);

            return viewModel;
        }

        public async Task<CategoryReviewListModel> PrepareListModelAsync(CategoryReviewSearchModel request, bool isCurrentUser = false, Guid? userId = null)
        {
            request.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            request.TotalCount = await _categoryReviewService.CountByRequestAsync(request);
            var categryReviews = await _categoryReviewService.GetByRequestAsync(request);
            var categoryReviewViewModel = _mapper.Map<IList<CategoryReviewModel>>(categryReviews);
            var categoryReviewList = new CategoryReviewListModel
            {
                SearchRequest = request,
                CategoryReviews = categoryReviewViewModel,
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                PageSizeList = await _listService.GetPageSizeListAsync(),
                ActiveList = EnumHelper.CastToSelectListItems<ActiveType>()
            };

            return categoryReviewList;
        }

        #endregion Public Methods
    }
}