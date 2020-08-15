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
    public class CompanyQuestionFactory : ICompanyQuestionFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly ICompanyQuestionService _companyQuestionService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public CompanyQuestionFactory(IMapper mapper, ICompanyQuestionService companyQuestionService, ICommonService commonService, IListService listService)
        {
            _mapper = mapper;
            _companyQuestionService = companyQuestionService;
            _commonService = commonService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<CompanyQuestionDetailModel> PrepareDetailModelAsync(Guid companyQuestionId)
        {
            var companyQuestion = await _companyQuestionService.FindByIdAsync(companyQuestionId);
            var viewModel = _mapper.Map<CompanyQuestionDetailModel>(companyQuestion);

            return viewModel;
        }

        public async Task<CompanyQuestionEditModel> PrepareEditViewModelAsync(Guid companyQuestionId)
        {
            var companyQuestion = await _companyQuestionService.FindByIdAsync(companyQuestionId);
            var viewModel = _mapper.Map<CompanyQuestionEditModel>(companyQuestion);

            return viewModel;
        }

        public async Task<CompanyQuestionListModel> PrepareListModelAsync(CompanyQuestionSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _companyQuestionService.CountByRequestAsync(model);
            var companyQuestions = await _companyQuestionService.GetByRequestAsync(model);
            var companyQuestionViewModel = _mapper.Map<IList<CompanyQuestionModel>>(companyQuestions);
            var companyQuestionList = new CompanyQuestionListModel
            {
                SearchModel = model,
                CompanyQuestions = companyQuestionViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                StateTypeList = EnumHelper.CastToSelectListItems<StateType>()
            };

            if (isCurrentUser)
                companyQuestionList.IsMine = true;

            return companyQuestionList;
        }

        #endregion Public Methods
    }
}