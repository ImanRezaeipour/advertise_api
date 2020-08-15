using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Exceptions;
using Advertise.Core.Helpers;
using Advertise.Core.Model.Companies;
using Advertise.Core.Types;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using AutoMapper;

namespace Advertise.Service.Factory.Companies
{
    public class CompanyOfficialFactory : ICompanyOfficialFactory
    {
        #region Private Fields

        private readonly ICompanyOfficialService _companyOfficialService;
        private readonly IMapper _mapper;
        private readonly IListService _listService;
        private readonly ICommonService _commonService;

        #endregion Private Fields

        #region Public Constructors

        public CompanyOfficialFactory(ICompanyOfficialService companyOfficialService, IMapper mapper, ICommonService commonService, IListService listService)
        {
            _companyOfficialService = companyOfficialService;
            _mapper = mapper;
            _commonService = commonService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CompanyOfficialEditModel> PrepareEditModelAsync(Guid companyOfficialId, bool applyCurrentUser = false, CompanyOfficialEditModel modelApply = null)
        {
            var companyOfficial = await _companyOfficialService.FindByIdAsync(companyOfficialId);
            if (companyOfficial == null)
                throw new FactoryException();

            var viewModel = modelApply ?? _mapper.Map<CompanyOfficialEditModel>(companyOfficial);

            if (applyCurrentUser)
                viewModel.IsMine = true;
            return viewModel;
        }

        public async Task<CompanyOfficialListModel> PrepareListModelAsync(CompanyOfficialSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _companyOfficialService.CountByRequestAsync(model);
            var companies = await _companyOfficialService.GetByRequestAsync(model);
            var companyViewModel = _mapper.Map<IList<CompanyOfficialModel>>(companies);
            var companyList = new CompanyOfficialListModel
            {
                SearchModel = model,
                CompanyOfficials = companyViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                SortMemberList = await _listService.GetSortMemberListByTitleAsync(),
                StateList = EnumHelper.CastToSelectListItems<ActiveType>()
            };

            if (isCurrentUser)
                companyList.CompanyOfficials.ForEach(p => p.IsMine = true);

            return companyList;
        }

        #endregion Public Methods
    }
}