using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Companies;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using Advertise.Service.Products;
using AutoMapper;

namespace Advertise.Service.Factory.Companies
{
    public class CompanySlideFactory : ICompanySlideFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly ICompanySlideService _companySlideService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IWebContextManager _webContextManager;
        private readonly ICompanyService _companyService;

        #endregion Private Fields

        #region Public Constructors

        public CompanySlideFactory(ICompanySlideService companySlideService, IMapper mapper, IProductService productService, ICommonService commonService, IWebContextManager webContextManager, IListService listService, ICompanyService companyService)
        {
            _companySlideService = companySlideService;
            _mapper = mapper;
            _productService = productService;
            _commonService = commonService;
            _webContextManager = webContextManager;
            _listService = listService;
            _companyService = companyService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CompanySlideCreateModel> PrepareCreateModelAsync()
        {
            var viewModel = new CompanySlideCreateModel
            {
                EntityList = await _productService.GetAllCurrentUserAsSelectListItem()
            };

            return viewModel;
        }

        public async Task<CompanySlideEditModel> PrepareEditModelAsync(Guid companySlideId)
        {
            var companySlide = await _companySlideService.FindByIdAsync(companySlideId);
            var companySlideViewModel = _mapper.Map<CompanySlideEditModel>(companySlide);
            companySlideViewModel.EntityList = await _productService.GetAllCurrentUserAsSelectListItem();

            return companySlideViewModel;
        }

        public async Task<CompanySlideBulkEditModel> PrepareBulkEditModelAsync()
        {
            var request = new CompanySlideSearchModel
            {
                CompanyId = _companyService.CurrentCompanyId
            };
            var companySlides = await _companySlideService.GetByRequestAsync(request);
            var companySlideViewModel = _mapper.Map<IList<CompanySlideModel>>(companySlides);
            var viewModel = new CompanySlideBulkEditModel
            {
                ProductList = await _productService.GetAllCurrentUserAsSelectListItem(),
                SlideList = companySlideViewModel
            };

            return viewModel;
        }

        public async Task<CompanySlideListModel> PrepareListModelAsync(CompanySlideSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CompanyId = _companyService.CurrentCompanyId;
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _companySlideService.CountByRequestAsync(model);
            var companySlides = await _companySlideService.GetByRequestAsync(model);
            var companySlideViewModel = _mapper.Map<IList<CompanySlideModel>>(companySlides);
            var companySlideList = new CompanySlideListModel
            {
                SearchModel = model,
                CompanySlides = companySlideViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                SortMemberList = await _listService.GetSortMemberListByTitleAsync()
            };

            if (isCurrentUser)
            {
                companySlideList.IsMine = true;
                companySlideList.CompanySlides.ForEach(p => p.IsMine = true);
            }
               
            return companySlideList;
        }

        #endregion Public Methods
    }
}