using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Categories;
using Advertise.Service.Categories;
using Advertise.Service.Common;
using AutoMapper;

namespace Advertise.Service.Factory.Categories
{
    public class CategoryFollowFactory : ICategoryFollowFactory
    {
        #region Private Fields

        private readonly ICategoryFollowService _categoryFollowService;
        private readonly IWebContextManager _webContextManager;
        private readonly IMapper _mapper;
        private readonly IListService _listService;

        #endregion Private Fields

        #region Public Constructors

        public CategoryFollowFactory(ICategoryFollowService categoryFollowService, IWebContextManager webContextManager, IMapper mapper, IListService listService)
        {
            _categoryFollowService = categoryFollowService;
            _webContextManager = webContextManager;
            _mapper = mapper;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CategoryFollowListModel> PrepareListModelAsync(bool isCurrentUser = false, Guid? userId = null)
        {
            var request = new CategoryFollowSearchModel
            {
                IsFollow = true
            };
            if (isCurrentUser)
                request.FollowedById = _webContextManager.CurrentUserId;
            else if (userId != null)
                request.FollowedById = userId;
            else
                request.FollowedById = null;
            var categoryFollows = await _categoryFollowService.GetByRequestAsync(request);
            request.TotalCount = categoryFollows.Count;
            var categoryFollowsViewModel = _mapper.Map<IList<CategoryFollowModel>>(categoryFollows);
            var listVieModel = new CategoryFollowListModel
            {
                SearchRequest = request,
                CategoryFollows = categoryFollowsViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync()
            };

            return listVieModel;
        }

        #endregion Public Methods
    }
}