using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Helpers;
using Advertise.Core.Model.Announces;
using Advertise.Core.Types;
using Advertise.Service.Announces;
using Advertise.Service.Common;
using AutoMapper;

namespace Advertise.Service.Factory.Announces
{
    public class AnnounceOptionFactory : IAnnounceOptionFactory
    {
        #region Private Fields

        private readonly IAnnounceOptionService _announceOptionService;
        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public AnnounceOptionFactory(ICommonService commonService, IAnnounceOptionService adsOptionService, IMapper mapper, IListService listService)
        {
            _commonService = commonService;
            _announceOptionService = adsOptionService;
            _mapper = mapper;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<AnnounceOptionCreateModel> PrepareCreateModelAsync(AnnounceOptionCreateModel viewModelPrepare = null)
        {
            var viewModel = viewModelPrepare ?? new AnnounceOptionCreateModel();
            viewModel.TypeList = EnumHelper.CastToSelectListItems<AnnounceType>();
            viewModel.PositionList = EnumHelper.CastToSelectListItems<AnnouncePositionType>();

            return viewModel;
        }

        public async Task<AnnounceOptionEditModel> PrepareEditModelAsync(Guid id)
        {
            var adsOption = await _announceOptionService.FindByIdAsync(id);
            var viewModel = _mapper.Map<AnnounceOptionEditModel>(adsOption);
            viewModel.TypeList = EnumHelper.CastToSelectListItems<AnnounceType>();
            viewModel.PositionList = EnumHelper.CastToSelectListItems<AnnouncePositionType>();

            return viewModel;
        }

        public async Task<AnnounceOptionListModel> PrepareListModelAsync(AnnounceOptionSearchModel request, bool isCurrentUser = false, Guid? userId = null)
        {
            request.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            var categories = await _announceOptionService.GetByRequestAsync(request);
            var categoryList = _mapper.Map<List<AnnounceOptionModel>>(categories);
            request.TotalCount = categories.Count;
            var categoriesList = new AnnounceOptionListModel
            {
                SearchRequest = request,
                AnnounceOptions = categoryList,
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                PageSizeList = await _listService.GetPageSizeListAsync()
            };

            return categoriesList;
        }

        #endregion Public Methods
    }
}