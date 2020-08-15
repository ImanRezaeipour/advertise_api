using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Companies;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using AutoMapper;

namespace Advertise.Service.Factory.Companies
{
    public class CompanyBalanceFactory : ICompanyBalanceFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly ICompanyBalanceService _companyBalanceService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;

        #endregion Private Fields

        #region Public Constructors

        public CompanyBalanceFactory(ICompanyBalanceService companyBalanceService, IMapper mapper, ICommonService commonService, ICompanyService companyService, IListService listService)
        {
            _companyBalanceService = companyBalanceService;
            _mapper = mapper;
            _commonService = commonService;
            _companyService = companyService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CompanyBalanceCreateModel> PrepareCreateModelAsync(CompanyBalanceCreateModel viewModelPrepare = null)
        {
            var viewModel = viewModelPrepare??  new CompanyBalanceCreateModel();

            viewModel.CompanyList = await _companyService.GetAllAsSelectListItemAsync();
            return viewModel;
        }

        public async Task<CompanyBalanceEditModel> PrepareEditModelAsync(Guid companyBalanceId, CompanyBalanceEditModel modelPrepare= null)
        {
            var viewModel = modelPrepare;
            if (viewModel == null)
            {
                var companyBalance = await _companyBalanceService.FindByIdAsync(companyBalanceId);
                viewModel = _mapper.Map<CompanyBalanceEditModel>(companyBalance);
            }
            viewModel.CompanyList = await _companyService.GetAllAsSelectListItemAsync();

            return viewModel;
        }

        public async Task<CompanyBalanceListModel> PrepareListModelAsync(CompanyBalanceSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            var userIdc = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.CreatedById = userIdc;
            if (userIdc != null)
                model.CompanyId = (await _companyService.FindByUserIdAsync(userIdc.Value)).Id;
            model.TotalCount = await _companyBalanceService.CountByRequestAsync(model);
            var companyBalances = await _companyBalanceService.GetByRequestAsync(model);
            var companyBalancesViewModel = _mapper.Map<IList<CompanyBalanceViewModel>>(companyBalances);
            var listViewModel = new CompanyBalanceListModel
            {
                SearchModel = model,
                CompanyBalances = companyBalancesViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
            };

            if (isCurrentUser)
                listViewModel.IsMine = true;

            return listViewModel;
        }

        public async Task<CompanyBalanceViewModel> PrepareModelAsync()
        {
            var viewModel = new CompanyBalanceViewModel();

            return viewModel;
        }

        #endregion Public Methods
    }
}