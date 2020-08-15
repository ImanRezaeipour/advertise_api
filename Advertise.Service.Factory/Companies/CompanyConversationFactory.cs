using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Companies;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using AutoMapper;

namespace Advertise.Service.Factory.Companies
{
    public class CompanyConversationFactory : ICompanyConversationFactory
    {
        #region Private Fields

        private readonly ICompanyConversationService _companyConversationService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CompanyConversationFactory(ICompanyConversationService companyConversationService, IMapper mapper, IWebContextManager webContextManager, IListService listService)
        {
            _companyConversationService = companyConversationService;
            _mapper = mapper;
            _webContextManager = webContextManager;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CompanyConversationEditModel> PrepareEditModelAsync(Guid conversationId)
        {
            var conversation = await _companyConversationService.FindByIdAsync(conversationId);
            var viewModel = _mapper.Map<CompanyConversationEditModel>(conversation);

            return viewModel;
        }

        public async Task<CompanyConversationListModel> PrepareListModelAsync(CompanyConversationSearchModel model,bool isCurrentUser = false, Guid? userId = null)
        {
            if (isCurrentUser)
                model.CreatedById = _webContextManager.CurrentUserId;
            else if (userId != null)
                model.CreatedById = userId;
            else
                model.CreatedById = null;
            model.TotalCount = await _companyConversationService.CountByRequestAsync(model);
            var conversations = await _companyConversationService.GetByRequestAsync(model);
            var conversationViewModel = _mapper.Map<IList<CompanyConversationModel>>(conversations);
            var conversationList = new CompanyConversationListModel
            {
                SearchModel = model,
                Conversations = conversationViewModel,
                CreatedIdConversationList = await _companyConversationService.GetUsersAsSelectListAsync(),
                OwnedById = _webContextManager.CurrentUserId
            };

            if (isCurrentUser)
                conversationList.IsMine = true;

            return conversationList;
        }

        #endregion Public Methods
    }
}