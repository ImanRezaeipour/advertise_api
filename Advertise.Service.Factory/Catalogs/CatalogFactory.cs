using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Model.Catalogs;
using Advertise.Service.Catalogs;
using Advertise.Service.Common;
using Advertise.Service.Keywords;
using Advertise.Service.Specifications;
using AutoMapper;

namespace Advertise.Service.Factory.Catalogs
{
    public class CatalogFactory : ICatalogFactory
    {
        #region Private Fields

        private readonly ICatalogService _catalogService;
        private readonly ICatalogSpecificationService _catalogSpecificationService;
        private readonly ISpecificationService _specificationService;
        private readonly ICommonService _commonService;
        private readonly IKeywordService _keywordService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public CatalogFactory(ICommonService commonService, IMapper mapper, IKeywordService keywordService, ICatalogService catalogService, ICatalogSpecificationService catalogSpecificationService, ISpecificationService specificationService, IListService listService)
        {
            _commonService = commonService;
            _mapper = mapper;
            _keywordService = keywordService;
            _catalogService = catalogService;
            _catalogSpecificationService = catalogSpecificationService;
            _specificationService = specificationService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CatalogCreateModel> PrepareCreateModelAsync(CatalogCreateModel viewModelPrepare = null)
        {
            var viewModel = viewModelPrepare ?? new CatalogCreateModel();

            viewModel.KeywordList = await _keywordService.GetAllActiveAsSelectListAsync();

            return viewModel;
        }

        public async Task<CatalogEditModel> PrepareEditModelAsync(Guid catalogId)
        {
            var catalog = await _catalogService.FindByIdAsync(catalogId);
            var viewModel = _mapper.Map<CatalogEditModel>(catalog);

            var allSpecifications = await _specificationService.GetByCategoryIdAsync(viewModel.CategoryId);
            var catalogSpecificationViewModel = _mapper.Map<IList<CatalogSpecificationModel>>(allSpecifications);
            var catalogSpecifications = await _catalogSpecificationService.GetByCatalogIdAsync(catalogId);

            //var f = _mapper.Map<IList<CatalogSpecificationViewModel>>(viewModel.Specifications);
            //for (var i = 0; i < f.Capacity; i++)
            //{
            //    f[i].SpecificationOptionIdList = productSpecifications.Where(model => model.SpecificationId == f[i].Id)
            //        .Select(model => model.SpecificationOptionId.Value).ToList();
            //}
            
            foreach (var item in catalogSpecificationViewModel)
            {
                item.Value = catalogSpecifications.FirstOrDefault(model => model.SpecificationId == item.Id)?.Value;
                item.SpecificationOptionIdList = catalogSpecifications.Where(model => model.SpecificationId == item.Id)
                    .Select(model => model.SpecificationOptionId).ToList();
            }
            viewModel.Specifications = catalogSpecificationViewModel;

            viewModel.KeywordList = await _keywordService.GetAllActiveAsSelectListAsync();

            return viewModel;
        }

        public async Task<CatalogListModel> PrepareListModelAsync(CatalogSearchModel request, bool isCurrentUser = false, Guid? userId = null)
        {
            request.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            var catalogs = await _catalogService.GetByRequestAsync(request);
            var catalogViewModel = _mapper.Map<IList<CatalogModel>>(catalogs);
            request.TotalCount = await _catalogService.CountByRequestAsync(request);

            var viewModel = new CatalogListModel
            {
                Catalogs = catalogViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                SearchRequest = request
            };

            if (isCurrentUser)
            {
                viewModel.IsMine = true;
                viewModel.Catalogs.ForEach(p => p.IsMine = true);
            }

            return viewModel;
        }

        #endregion Public Methods
    }
}