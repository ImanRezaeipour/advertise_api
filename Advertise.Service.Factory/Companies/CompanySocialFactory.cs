using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Companies;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using AutoMapper;

namespace Advertise.Service.Factory.Companies
{
    public class CompanySocialFactory : ICompanySocialFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly ICompanyService _companyService;
        private readonly ICompanySocialService _companySocialService;
        private readonly IMapper _mapper;
        private readonly IWebContextManager _webContextManager;
        private readonly IListService _listService;

        #endregion Private Fields

        #region Public Constructors

        public CompanySocialFactory(ICompanySocialService companySocialService, IMapper mapper, IWebContextManager webContextManager, ICompanyService companyService, ICommonService commonService, IListService listService)
        {
            _companySocialService = companySocialService;
            _mapper = mapper;
            _webContextManager = webContextManager;
            _companyService = companyService;
            _commonService = commonService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CompanySocialEditModel> PrepareEditModelAsync(Guid? companySocialId = null, bool isCurrentUser = false, CompanySocialEditModel modelPrepare = null)
        {
            var viewModel = modelPrepare;
            CompanySocial companySocial;
            if (viewModel == null)
            {
                companySocial = await _companySocialService.FindAsync(companySocialId.GetValueOrDefault());
                viewModel = _mapper.Map<CompanySocialEditModel>(companySocial);
            }
            if (isCurrentUser)
            {
                companySocial = await _companySocialService.FindByUserIdAsync(_webContextManager.CurrentUserId);
                viewModel = _mapper.Map<CompanySocialEditModel>(companySocial);
                if (viewModel == null)
                {
                    var companya = await _companyService.FindByUserIdAsync(_webContextManager.CurrentUserId);
                    viewModel = new CompanySocialEditModel
                    {
                        CompanyId = companya.Id
                    };
                    viewModel.IsMine = true;
                    return viewModel;
                }

                viewModel.IsMine = true;
            }
            
            return viewModel;
        }

        public async Task<CompanySocialListModel> PrepareListModelAsync(CompanySocialSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _companySocialService.CountByRequestAsync(model);
            var companySocials = await _companySocialService.GetByRequestAsync(model);
            var companySocialViewModel = _mapper.Map<IList<CompanySocialModel>>(companySocials);
            var companySocialList = new CompanySocialListModel
            {
                SearchModel = model,
                CompanySocials = companySocialViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                SortMemberList = await _listService.GetSortMemberListByTitleAsync()
            };

            return companySocialList;
        }

        #endregion Public Methods
    }
}