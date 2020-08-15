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
    public class CompanyImageFactory : ICompanyImageFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly ICompanyImageService _companyImageService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public CompanyImageFactory(ICommonService commonService, ICompanyImageService companyImageService, IMapper mapper, IListService listService)
        {
            _commonService = commonService;
            _companyImageService = companyImageService;
            _mapper = mapper;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CompanyImageEditModel> PrepareEditModelAsync(Guid companyImageId, bool applyCurrentUser = false, CompanyImageEditModel  modelPrepare = null)
        {
            var isMine = await _companyImageService.IsMineAsync(companyImageId);
            if (!isMine)
                throw new FactoryException("عدم دسترسی");

            var companyImage = await _companyImageService.FindByIdAsync(companyImageId);
            var viewModel = modelPrepare ?? _mapper.Map<CompanyImageEditModel>(companyImage);

            if (applyCurrentUser)
                viewModel.IsMine = true;

            return viewModel;
        }

        public async Task<CompanyImageListModel> PrepareListModelAsync(CompanyImageSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _companyImageService.CountByRequestAsync(model);
            var companyImages = await _companyImageService.GetByRequestAsync(model);
            var companyImageViewModel = _mapper.Map<IList<CompanyImageModel>>(companyImages);
            var companyImageList = new CompanyImageListModel
            {
                SearchModel = model,
                CompanyImages = companyImageViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                StateList = EnumHelper.CastToSelectListItems<StateType>()
            };

            if (isCurrentUser)
            {
                companyImageList.IsMine = true;
                companyImageList.CompanyImages.ForEach(p => p.IsMine = true);
            }

            return companyImageList;
        }

        #endregion Public Methods
    }
}