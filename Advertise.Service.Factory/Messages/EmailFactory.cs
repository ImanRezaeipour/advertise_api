using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Emails;
using Advertise.Service.Common;
using Advertise.Service.Messages;
using AutoMapper;

namespace Advertise.Service.Factory.Messages
{
    public class EmailFactory : IEmailFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IEmailService _emailService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public EmailFactory(IEmailService emailService, ICommonService commonService, IMapper mapper, IListService listService)
        {
            _emailService = emailService;
            _commonService = commonService;
            _mapper = mapper;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<EmailListModel> PrepareListViewModelAsync(EmailSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _emailService.CountByRequestAsync(model);
            var emails = await _emailService.GetByRequestAsync(model);
            var emailViewModel = _mapper.Map<IList<EmailModel>>(emails);
            var viewModel = new EmailListModel
            {
                SearchModel = model,
                Emails = emailViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync()
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}