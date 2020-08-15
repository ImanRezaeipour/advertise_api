using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Helpers;
using Advertise.Core.Model.Newsletters;
using Advertise.Core.Types;
using Advertise.Service.Common;
using Advertise.Service.Newsletters;
using AutoMapper;

namespace Advertise.Service.Factory.Newsletters
{
    public class NewsletterFactory : INewsletterFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly INewsletterService _newsletterService;

        #endregion Private Fields

        #region Public Constructors

        public NewsletterFactory(ICommonService commonService, IMapper mapper, INewsletterService newsletterService, IListService listService)
        {
            _commonService = commonService;
            _mapper = mapper;
            _newsletterService = newsletterService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<NewsletterCreateModel> PrepareCreateModelAsync(NewsletterCreateModel modelPrepare= null)
        {
            var viewModel = modelPrepare?? new NewsletterCreateModel();

            viewModel.MeetList = EnumHelper.CastToSelectListItems<MeetType>();
            return viewModel;
        }

        public async Task<NewsletterListModel> PrepareListModelAsync(NewsletterSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _newsletterService.CountByRequestAsync(model);
            var newsletters = await _newsletterService.GetByRequestAsync(model);
            var newsletterViewModel = _mapper.Map<IList<NewsletterModel>>(newsletters);
            var newsnletterList = new NewsletterListModel
            {
                Newsletter = newsletterViewModel,
                SearchModel = model,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                MeetList = EnumHelper.CastToSelectListItems<MeetType>()
            };

            return newsnletterList;
        }

        #endregion Public Methods
    }
}