using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Smses;
using Advertise.Service.Common;
using Advertise.Service.Messages;
using AutoMapper;

namespace Advertise.Service.Factory.Messages
{
    public class SmsFactory : ISmsFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly ISmsService _smsService;

        #endregion Private Fields

        #region Public Constructors

        public SmsFactory(ISmsService smsService, IMapper mapper, ICommonService commonService, IListService listService)
        {
            _smsService = smsService;
            _mapper = mapper;
            _commonService = commonService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<SmsListViewModel> PrepareListViewModelAsync(SmsSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _smsService.CountByRequestAsync(model);
            var sms = await _smsService.GetByRequestAsync(model);
            var smsViewModel = _mapper.Map<IList<SmsModel>>(sms);
            var viewModel = new SmsListViewModel
            {
                SearchModel = model,
                Smses = smsViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync()
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}