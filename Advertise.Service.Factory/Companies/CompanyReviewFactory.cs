using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Helpers;
using Advertise.Core.Model.Companies;
using Advertise.Core.Types;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using AutoMapper;

namespace Advertise.Service.Factory.Companies
{
    public class CompanyReviewFactory : ICompanyReviewFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly ICompanyReviewService _companyReviewService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public CompanyReviewFactory(ICommonService commonService, ICompanyReviewService companyReviewService, IMapper mapper, IListService listService)
        {
            _commonService = commonService;
            _companyReviewService = companyReviewService;
            _mapper = mapper;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CompanyReviewDetailModel> PrepareDetailModelAsync(Guid companyReviewId)
        {
            var companyReview = await _companyReviewService.FindByIdAsync(companyReviewId);
            var viewModel = _mapper.Map<CompanyReviewDetailModel>(companyReview);

            return viewModel;
        }

        public async Task<CompanyReviewEditModel> PrepareEditViewModelAsync(Guid companyReviewId)
        {
            var companyReview = await _companyReviewService.FindByIdAsync(companyReviewId);
            var viewModel = _mapper.Map<CompanyReviewEditModel>(companyReview);
            return viewModel;
        }

        public async Task<CompanyReviewListModel> PrepareListModelAsync(CompanyReviewSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _companyReviewService.CountByRequestAsync(model);
            var companyReviews = await _companyReviewService.GetByRequestAsync(model);
            var companyReviewViewModel = _mapper.Map<IList<CompanyReviewModel>>(companyReviews);
            var companyReviewList = new CompanyReviewListModel
            {
                SearchModel = model,
                CompanyReviews = companyReviewViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                ActiveList = EnumHelper.CastToSelectListItems<ActiveType>()
            };

            return companyReviewList;
        }

        public async Task<CompanyReviewCreateModel> PrepareCreateModelAsync(CompanyReviewCreateModel modelPrepare = null)
        {
            var viewModel = modelPrepare ?? new CompanyReviewCreateModel();

            viewModel.CompanyList = await _companyReviewService.GetCompanyAsSelectListItemAsync();
            return viewModel;
        }

        #endregion Public Methods
    }
}