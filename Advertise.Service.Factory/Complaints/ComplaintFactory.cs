using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Model.Complaints;
using Advertise.Service.Common;
using Advertise.Service.Complaints;
using AutoMapper;

namespace Advertise.Service.Factory.Complaints
{
    public class ComplaintFactory : IComplaintFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IComplaintService _complaintService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public ComplaintFactory(IComplaintService complaintService, ICommonService commonService, IMapper mapper, IListService listService)
        {
            _complaintService = complaintService;
            _commonService = commonService;
            _mapper = mapper;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ComplaintDetailModel> PrepareDetailModelAsync(Guid companyId)
        {
            var complaint = await _complaintService.FindByIdAsync(companyId);
            var viewModel = _mapper.Map<ComplaintDetailModel>(complaint);

            return viewModel;
        }

        public async Task<ComplaintListModel> PrepareListModelAsync(ComplaintSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _complaintService.CountByRequestAsync(model);
            var complaints = await _complaintService.GetByRequestAsync(model);
            var complaintViewModel = _mapper.Map<IList<ComplaintModel>>(complaints);
            var companyList = new ComplaintListModel
            {
                SearchModel = model,
                Complaints = complaintViewModel,
                PageSizeList = await _listService.GetPageSizeListAsync(),
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                SortMemberList = await _listService.GetSortMemberListByTitleAsync()
            };

            return companyList;
        }

        #endregion Public Methods
    }
}