using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Helpers;
using Advertise.Core.Model.Seos;
using Advertise.Core.Types;
using Advertise.Service.Common;
using Advertise.Service.Seos;
using AutoMapper;

namespace Advertise.Service.Factory.Seos
{
    public class SeoUrlFactory : ISeoUrlFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IMapper _mapper;
        private readonly ISeoUrlService _seoUrlService;
        private readonly IListService _listService;

        #endregion Private Fields

        #region Public Constructors

        public SeoUrlFactory(ISeoUrlService seoUrlService, IMapper mapper, ICommonService commonService, IListService listService)
        {
            _seoUrlService = seoUrlService;
            _mapper = mapper;
            _commonService = commonService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<SeoUrlEditModel> PrepareEditModelAsync(Guid id, SeoUrlEditModel modelPrepare = null)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var seoUrl = await _seoUrlService.FindByIdAsync(id);
            var viewModel = modelPrepare ?? _mapper.Map<SeoUrlEditModel>(seoUrl);
            viewModel.RedirectionTypeList = EnumHelper.CastToSelectListItems<RedirectionType>();

            return viewModel;
        }

        public async Task<SeoUrlListModel> PrepareListModelAsync(SeoUrlSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _seoUrlService.CountByRequestAsync(model);
            var seoUrls = await _seoUrlService.GetByRequestAsync(model);
            var seoUrlsViewModel = _mapper.Map<List<SeoUrlModel>>(seoUrls);
            var viewModel = new SeoUrlListModel
            {
                SeoUrls = seoUrlsViewModel,
                SearchModel = model
            };

            viewModel.PageSizeList = await _listService.GetPageSizeListAsync();
            viewModel.SortDirectionList = await _listService.GetSortDirectionListAsync();

            return viewModel;
        }


        public async Task<SeoUrlCreateModel> PrepareCreateModelAsync(SeoUrlCreateModel modelPrepare= null)
        {
            var viewModel = new SeoUrlCreateModel
            {
                RedirectionTypeList = EnumHelper.CastToSelectListItems<RedirectionType>()
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}