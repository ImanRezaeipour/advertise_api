using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Complaints;
using Advertise.Service.Complaints;
using Advertise.Service.Factory.Complaints;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class ComplaintController : BaseController
    {
        #region Private Fields

        private readonly IComplaintFactory _complaintFactory;
        private readonly IComplaintService _complaintService;

        #endregion Private Fields

        #region Public Constructors

        public ComplaintController(IComplaintService complaintService, IComplaintFactory complaintFactory)
        {
            _complaintService = complaintService;
            _complaintFactory = complaintFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        public virtual async Task<HttpResponseMessage> Create(ComplaintCreateModel viewModel)
        {
            await _complaintService.CreateByViewModel(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanComplaintDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _complaintService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public virtual async Task<HttpResponseMessage> Detail(RequestModel model)
        {
            var viewModel = await _complaintFactory.PrepareDetailModelAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanComplaintList)]
        public virtual async Task<HttpResponseMessage> List(ComplaintSearchModel request)
        {
            var viewModel = await _complaintFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}