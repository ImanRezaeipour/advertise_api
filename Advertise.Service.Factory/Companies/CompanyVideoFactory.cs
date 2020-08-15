using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Helpers;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Companies;
using Advertise.Core.Types;
using Advertise.Service.Common;
using Advertise.Service.Companies;
using AutoMapper;

namespace Advertise.Service.Factory.Companies
{
    public class CompanyVideoFactory : ICompanyVideoFactory
    {
        #region Private Fields

        private readonly ICompanyVideoService _companyVideoService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly IWebContextManager _webContextManager;
        private readonly ICompanyService _companyService;

        #endregion Private Fields

        #region Public Constructors

        public CompanyVideoFactory(ICompanyVideoService companyVideoService, IMapper mapper, IWebContextManager webContextManager, IListService listService, ICompanyService companyService)
        {
            _companyVideoService = companyVideoService;
            _mapper = mapper;
            _webContextManager = webContextManager;
            _listService = listService;
            _companyService = companyService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CompanyVideoEditModel> PrepareEditModelAsync(Guid companyVideoId, bool applyCurrentUser = false)
        {
            var companyVideo = await _companyVideoService.FindByIdAsync(companyVideoId);
            var viewModel = _mapper.Map<CompanyVideoEditModel>(companyVideo);

            if (applyCurrentUser)
                viewModel.IsMine = true;

            return viewModel;
        }

        public async Task<CompanyVideoListModel> PrepareListModelAsync(CompanyVideoSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            if (isCurrentUser)
                model.CreatedById = _webContextManager.CurrentUserId;
            else if (userId != null)
                model.CreatedById = userId;
            else
                model.CreatedById = null;
            model.TotalCount = await _companyVideoService.CountByRequestAsync(model);
            var companyVideos = await _companyVideoService.GetByRequestAsync(model);
            var companyVideoViewModel = _mapper.Map<IList<CompanyVideoModel>>(companyVideos);
            var companyVideoList = new CompanyVideoListModel
            {
                SearchModel = model,
                CompanyVideos = companyVideoViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                StateList = EnumHelper.CastToSelectListItems<StateType>()
            };

            if (isCurrentUser)
            {
                companyVideoList.IsMine = true;
                companyVideoList.CompanyVideos.ForEach(p => p.IsMine = true);
            }

            return companyVideoList;
        }

        public async Task<CompanyVideoDetailModel> PrepareDetailModelAsync(Guid companyVideoId)
        {
            var companyVideo = await _companyVideoService.FindByIdAsync(companyVideoId);

            var viewModel = _mapper.Map<CompanyVideoDetailModel>(companyVideo);
            var request = new CompanyVideoSearchModel
            {
                CompanyId = _companyService.CurrentCompanyId,
                State = StateType.Approved
            };
            viewModel.Galleries = _mapper.Map<IList<CompanyVideoModel>>(await _companyVideoService.GetByRequestAsync(request));
            return viewModel;
        }

        #endregion Public Methods
    }
}