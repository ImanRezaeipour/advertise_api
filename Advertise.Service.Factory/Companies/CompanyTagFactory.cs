using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Companies;
using Advertise.Service.Common;
using Advertise.Service.Companies;

namespace Advertise.Service.Factory.Companies
{
    public class CompanyTagFactory : ICompanyTagFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly ICompanyTagService _companyTagService;
        private readonly IListService _listService;

        #endregion Private Fields

        #region Public Constructors

        public CompanyTagFactory(ICompanyTagService companyTagService, ICommonService commonService, IListService listService)
        {
            _companyTagService = companyTagService;
            _commonService = commonService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CompanyTagListModel> PrepareListModelAsync(CompanyTagSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            var viewModel = await _companyTagService.ListByRequestAsync(model);
            viewModel.PageSizeList = await _listService.GetPageSizeListAsync();
            viewModel.SortDirectionList = await _listService.GetSortDirectionListAsync();

            return viewModel;
        }

        #endregion Public Methods
    }
}