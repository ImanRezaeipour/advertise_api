using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Helpers;
using Advertise.Core.Model.Notices;
using Advertise.Core.Types;
using Advertise.Service.Common;
using Advertise.Service.Notices;
using AutoMapper;

namespace Advertise.Service.Factory.Notices
{
    public class NoticeFactory : INoticeFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly INoticeService _noticeService;

        #endregion Private Fields

        #region Public Constructors

        public NoticeFactory(IMapper mapper, ICommonService commonService, INoticeService noticeService, IListService listService)
        {
            _mapper = mapper;
            _commonService = commonService;
            _noticeService = noticeService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<NoticeEditModel> PrepareEditModelAsync(Guid newsId)
        {
            var news = await _noticeService.FindByIdAsync(newsId);
            var viewModel = _mapper.Map<NoticeEditModel>(news);

            return viewModel;
        }

        public async Task<NoticeListModel> PrepareListModelAsync(NoticeSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _noticeService.CountByRequestAsync(model);
            var newses = await _noticeService.GetByRequestAsync(model);
            var newsViewModel = _mapper.Map<IList<NoticeModel>>(newses);
            var viewModel = new NoticeListModel
            {
                SearchModel = model,
                Notices = newsViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                ActiveList = EnumHelper.CastToSelectListItems<ActiveType>()
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}