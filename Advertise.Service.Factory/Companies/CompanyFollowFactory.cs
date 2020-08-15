using System;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Model.Companies;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using Advertise.Service.Users;

namespace Advertise.Service.Factory.Companies
{
    public class CompanyFollowFactory : ICompanyFollowFactory
    {
        #region Private Fields

        private readonly ICompanyFollowService _companyFollowService;
        private readonly ICompanyService _companyService;
        private readonly IUserService _userService;
        private readonly IListService _listService;

        #endregion Private Fields

        #region Public Constructors

        public CompanyFollowFactory(ICompanyFollowService companyFollowService, IUserService userService, ICompanyService companyService, IListService listService)
        {
            _companyFollowService = companyFollowService;
            _userService = userService;
            _companyService = companyService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CompanyFollowListModel> PrepareListModelAsync(bool isCurrentUser = false, bool follower = false, Guid? userId = null)
        {
            CompanyFollowSearchModel model;
            if (follower)
            {
                var user = isCurrentUser ? await _userService.GetCurrentUserAsync() : await _userService.FindByIdAsync(userId.GetValueOrDefault());

                var company = await _companyService.FindByUserIdAsync(user.Id);

                model = new CompanyFollowSearchModel
                {
                    PageSize = PageSize.All,
                    CompanyId = company.Id,
                    IsFollow = true
                };
            }
            else
            {
                model = new CompanyFollowSearchModel
                {
                    PageSize = PageSize.All,
                    IsFollow = true

                };
            }

            var listViewModel = await _companyFollowService.ListByRequestAsync(model, isCurrentUser, userId);
            listViewModel.PageSizeList = await _listService.GetPageSizeListAsync();
            return listViewModel;
        }

        #endregion Public Methods
    }
}