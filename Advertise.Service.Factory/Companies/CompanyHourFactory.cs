using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Helpers;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Companies;
using Advertise.Core.Types;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using AutoMapper;

namespace Advertise.Service.Factory.Companies
{
    public class CompanyHourFactory : ICompanyHourFactory
    {
        #region Private Fields

        private readonly ICompanyHourService _companyHourService;
        private readonly IMapper _mapper;
        private readonly IWebContextManager _webContextManager;
        private readonly IListService _listService;
        private readonly ICommonService _commonService;
        private readonly ICompanyService _companyService;

        #endregion Private Fields

        #region Public Constructors

 
        public CompanyHourFactory(ICompanyHourService companyHourService, IMapper mapper, IWebContextManager webContextManager, ICommonService commonService, IListService listService, ICompanyService companyService)
        {
            _companyHourService = companyHourService;
            _mapper = mapper;
            _webContextManager = webContextManager;
            _commonService = commonService;
            _listService = listService;
            _companyService = companyService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CompanyHourEditModel> PrepareEditModelAsync(Guid? companyHourId = null, bool isCurrentUser = false, CompanyHourEditModel modelPrepare = null)
        {
            var request = new CompanyHourSearchModel
            {
                CompanyId = _companyService.CurrentCompanyId,
            };
            var list = await _companyHourService.GetByRequestAsync(request);
            var listViewModel = _mapper.Map<IList<CompanyHourModel>>(list);
            var editViewModel = modelPrepare ?? new CompanyHourEditModel();
            editViewModel.CompanyHours = listViewModel;
            editViewModel.DayList = EnumHelper.CastToSelectListItems<DayType>();
            return editViewModel;
        }

        public async Task<CompanyHourListModel> PrepareListModRelAsync(CompanyHourSearchModel model,bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _companyHourService.CountByRequestAsync(model);
            var companies = await _companyHourService.GetByRequestAsync(model);
            var companyViewModel = _mapper.Map<IList<CompanyHourModel>>(companies);
            var companyList = new CompanyHourListModel
            {
                SearchModel = model,
                CompanyHours = companyViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                SortMemberList = await _listService.GetSortMemberListByTitleAsync(),
            };

            if (isCurrentUser)
                companyList.CompanyHours.ForEach(p => p.IsMine = true);

            return companyList;
        }

        #endregion Public Methods
    }
}