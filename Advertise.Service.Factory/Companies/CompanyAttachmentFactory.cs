using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Exceptions;
using Advertise.Core.Helpers;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Companies;
using Advertise.Core.Types;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using AutoMapper;

namespace Advertise.Service.Factory.Companies
{
    public class CompanyAttachmentFactory : ICompanyAttachmentFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly ICompanyAttachmentService _companyAttachmentService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IWebContextManager _webContextManager;
        private readonly ICompanyAttachmentFilService _companyAttachmentFilService;

        #endregion Private Fields

        #region Public Constructors

        public CompanyAttachmentFactory(ICompanyAttachmentService companyAttachmentService, ICommonService commonService, IMapper mapper, IWebContextManager webContextManager, ICompanyAttachmentFilService companyAttachmentFilService, IListService listService)
        {
            _companyAttachmentService = companyAttachmentService;
            _commonService = commonService;
            _mapper = mapper;
            _webContextManager = webContextManager;
            _companyAttachmentFilService = companyAttachmentFilService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CompanyAttachmentEditModel> PrepareEditModelAsync(Guid companyAttachmentId, bool applyCurrentUser = false)
        {
            var result = await _companyAttachmentService.IsMineAsync(companyAttachmentId);
            if (!result)
                throw new FactoryException("عدم دسترسی به این فایل");

            var companyAttachment = await _companyAttachmentService.FindByIdAsync(companyAttachmentId);
            var viewModel = _mapper.Map<CompanyAttachmentEditModel>(companyAttachment);

            if (applyCurrentUser)
                viewModel.IsMine = true;

            return viewModel;
        }

        public async Task<CompanyAttachmentListModel> PrepareListModelAsync(CompanyAttachmentSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            var companyAttachments = await _companyAttachmentService.GetByRequestAsync(model);
            model.TotalCount = await _companyAttachmentService.CountByRequestAsync(model);

            var companyAttachmentViewModel = _mapper.Map<IList<CompanyAttachmentModel>>(companyAttachments);
            var companyAttachmentList = new CompanyAttachmentListModel
            {
                SearchModel = model,
                CompanyAttachments = companyAttachmentViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                StateList = EnumHelper.CastToSelectListItems<StateType>()
            };

            if(isCurrentUser)
            {
                companyAttachmentList.IsMine = true;
                companyAttachmentList.CompanyAttachments.ForEach(p => p.IsMine = false);
            }

            return companyAttachmentList;
        }

        public async Task<CompanyAttachmentDetailModel> PrepareDetailModelAsync(Guid companyAttachmentId)
        {
            var companyAttachment = await _companyAttachmentService.FindByIdAsync(companyAttachmentId);

            var viewModel=  _mapper.Map<CompanyAttachmentDetailModel>(companyAttachment);
            var request = new CompanyAttachmentFileSearchModel
            {
                CompanyAttachmentId = companyAttachmentId
            };
            viewModel.Files = _mapper.Map<IList<CompanyAttachmentFileModel>>(await _companyAttachmentFilService.GetByRequestAsync(request));
            return viewModel;
        }
        #endregion Public Methods
    }
}