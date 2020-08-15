using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Helpers;
using Advertise.Core.Model.Specifications;
using Advertise.Core.Types;
using Advertise.Service.Common;
using Advertise.Service.Specifications;
using AutoMapper;

namespace Advertise.Service.Factory.Specifications
{
    public class SpecificationFactory : ISpecificationFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly ISpecificationService _specificationService;
        private readonly ISpecificationOptionService _specificationOptionService;

        #endregion Private Fields

        #region Public Constructors

        public SpecificationFactory(ICommonService commonService, IMapper mapper, ISpecificationService specificationService, ISpecificationOptionService specificationOptionService, IListService listService)
        {
            _commonService = commonService;
            _mapper = mapper;
            _specificationService = specificationService;
            _specificationOptionService = specificationOptionService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<SpecificationDetailModel> PrepareDetailModelAsync(Guid specificationId)
        {
            var specification = await _specificationService.FindByIdAsync(specificationId);
            var viewmodel = _mapper.Map<SpecificationDetailModel>(specification);

            return viewmodel;
        }

        public async Task<SpecificationCreateModel> PrepareCreateModelAsync(SpecificationCreateModel modelPrepare = null)
        {
            var viewModel = modelPrepare ?? new SpecificationCreateModel();
            viewModel.TypeList = EnumHelper.CastToSelectListItems<SpecificationType>();

            return viewModel;
        }

        public async Task<SpecificationEditModel> PrepareEditModelAsync(Guid specificationId, SpecificationEditModel modelPrepare = null)
        {
            var specification = await _specificationService.FindByIdAsync(specificationId);
            var viewModel = modelPrepare?? _mapper.Map<SpecificationEditModel>(specification);

            var specificationOptions = await _specificationOptionService.GetSpecificationOptionsBySpecificationIdAsync(specificationId);
            if(specificationOptions != null)
                viewModel.Options = _mapper.Map<IList<SpecificationOptionModel>>(specificationOptions);

            viewModel.TypeList = EnumHelper.CastToSelectListItems<SpecificationType>();

            return viewModel;
        }

        public async Task<SpecificationListModel> PrepareListViewModelAsync(SpecificationSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _specificationService.CountByRequestAsync(model);
            var specification = await _specificationService.GetByRequestAsync(model);
            var specificationViewModel = _mapper.Map<IList<SpecificationModel>>(specification);
            var viewModel = new SpecificationListModel
            {
                SearchModel = model,
                Specifications = specificationViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync()
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}