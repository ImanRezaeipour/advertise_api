using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Exceptions;
using Advertise.Core.Helpers;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Announces;
using Advertise.Core.Types;
using Advertise.Service.Announces;
using Advertise.Service.Categories;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using Advertise.Service.Products;
using AutoMapper;
using Newtonsoft.Json;

namespace Advertise.Service.Factory.Announces
{
    public class AnnounceFactory : IAnnounceFactory
    {
        #region Private Fields

        private readonly ICompanyService _companyService;
        private readonly IProductService _productService;
        private readonly IWebContextManager _webContextManager;
        private readonly IAnnounceService _announceService;
        private readonly IMapper _mapper;
        private readonly IAnnounceOptionService _announceOptionService;
        private readonly ICategoryService _categoryService;
        private readonly IListService _listService;

        #endregion Private Fields

        #region Public Constructors

        public AnnounceFactory(IProductService productService, ICompanyService companyService, IWebContextManager webContextManager, IAnnounceService adsService, IMapper mapper, IAnnounceOptionService adsOptionService, ICategoryService categoryService, IListService listService)
        {
            _productService = productService;
            _companyService = companyService;
            _webContextManager = webContextManager;
            _announceService = adsService;
            _mapper = mapper;
            _announceOptionService = adsOptionService;
            _categoryService = categoryService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<AnnounceCreateModel> PrepareCreateModel(AnnounceCreateModel viewModelPrepare = null)
        {
            var viewModel = viewModelPrepare ?? new AnnounceCreateModel();
            viewModel.EntityList = await _productService.GetAllCurrentUserAsSelectListItem();
            viewModel.DurationList = EnumHelper.CastToSelectListItems<DurationType>();
            viewModel.AdsOptionTypeList = EnumHelper.CastToSelectListItems<AnnounceType>();
            viewModel.AdsOptionPositionList = EnumHelper.CastToSelectListItems<AnnouncePositionType>();
            viewModel.CategeoryListJson = JsonConvert.SerializeObject(await _categoryService.GetAllowedAsSelect2ObjectAsync());

            var adsOptions = await _announceOptionService.GetByRequestAsync(new AnnounceOptionSearchModel());
            viewModel.AnnounceOptions = _mapper.Map<IList<AnnounceOptionModel>>(adsOptions);

            var company = await _companyService.FindByUserIdAsync(_webContextManager.CurrentUserId);
            if (company != null)
                viewModel.CategoryId = company.CategoryId.GetValueOrDefault();

            return viewModel;
        }

        public async Task<AnnounceEditModel> PrepareEditViewModelAsync(Guid id)
        {
            var ads = await _announceService.FindByIdAsync(id);
            if (ads == null)
                throw new FactoryException();

           return _mapper.Map<AnnounceEditModel>(ads);
        }

        #endregion Public Methods
    }
}