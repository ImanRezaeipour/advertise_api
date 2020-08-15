using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Newsletters;
using Advertise.Service.Factory.Newsletters;
using Advertise.Service.Newsletters;
using Advertise.Service.Services.Permissions;
using Advertise.Web.Filters;

namespace Advertise.Web.Api.Controllers
{
    public class NewsletterController : BaseController
    {
        #region Private Fields

        private readonly INewsletterFactory _newsletterFactory;
        private readonly INewsletterService _newsletterService;

        #endregion Private Fields

        #region Public Constructors
       
        public NewsletterController(INewsletterService newsletterService,  INewsletterFactory newsletterFactory)
        {
            _newsletterService = newsletterService;
            _newsletterFactory = newsletterFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> Create()
        {
            var viewModel = await _newsletterFactory.PrepareCreateModelAsync();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [HttpPost]
        public virtual async Task<HttpResponseMessage> Create(NewsletterCreateModel viewModel)
        {
            await _newsletterService.CreateByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        [MvcAuthorize(PermissionConst.CanNewsletterDeleteAjax)]
        public virtual async Task<HttpResponseMessage> DeleteAjax(RequestModel model)
        {
            await _newsletterService.DeleteByIdAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [MvcAuthorize(PermissionConst.CanNewsletterList)]
        public virtual async Task<HttpResponseMessage> List(NewsletterSearchModel request)
        {
            var viewModel = await _newsletterFactory.PrepareListModelAsync(request);
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        public virtual async Task<HttpResponseMessage> SubscribeAjax(NewsletterCreateModel viewModel)
        {
            await _newsletterService.SubscribeByViewModelAsync(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion Public Methods
    }
}