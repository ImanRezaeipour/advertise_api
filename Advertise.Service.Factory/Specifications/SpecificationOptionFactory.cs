using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Specifications;
using Advertise.Service.Common;
using Advertise.Service.Specifications;
using AutoMapper;

namespace Advertise.Service.Factory.Specifications
{
    public class SpecificationOptionFactory : ISpecificationOptionFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly ISpecificationOptionService _specificationOptionService;

        #endregion Private Fields

        #region Public Constructors

        public SpecificationOptionFactory(ICommonService commonService, IMapper mapper, ISpecificationOptionService specificationOptionService, IListService listService)
        {
            _commonService = commonService;
            _mapper = mapper;
            _specificationOptionService = specificationOptionService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<SpecificationOptionCreateModel> PrepareCreateModelAsync()
        {
            var viewModel = new SpecificationOptionCreateModel();

            return viewModel;
        }

        public async Task<SpecificationOptionDetailModel> PrepareDetailModelAsync(Guid specificationOptionId)
        {
            var specificationOption = await _specificationOptionService.FindByIdAsync(specificationOptionId);
            var viewModel = _mapper.Map<SpecificationOptionDetailModel>(specificationOption);

            return viewModel;
        }

        public async Task<SpecificationOptionEditModel> PrepareEditModelAsync(Guid specificationOptionId)
        {
            var specificationOption = await _specificationOptionService.FindWithCategoryAsync(specificationOptionId);
            var viewModel = _mapper.Map<SpecificationOptionEditModel>(specificationOption);

            return viewModel;
        }

        public async Task<SpecificationOptionListModel> PrepareListModelAsync(SpecificationOptionSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _specificationOptionService.CountByRequestAsync(model);
            var specificationOption = await _specificationOptionService.GetByRequestAsync(model);
            var specificationOptionViewModel = _mapper.Map<IList<SpecificationOptionModel>>(specificationOption);
            var viewModel = new SpecificationOptionListModel
            {
                SearchModel = model,
                SpecificationOptions = specificationOptionViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync()
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}