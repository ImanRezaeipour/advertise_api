using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advertise.Service.Announces;

namespace Advertise.Web.Api.Controllers
{
    public class AdsReserveController : BaseController
    {
        #region Private Fields

        private readonly IAnnounceReserveService _announceReserveService;

        #endregion Private Fields

        #region Public Constructors

        public AdsReserveController(IAnnounceReserveService announceReserveService)
        {
            _announceReserveService = announceReserveService;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> DateOfReleaseAjax(Guid optionId)
        {
            var result = await _announceReserveService.GetReserveDayByOptionIdAsync(optionId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #endregion Public Methods
    }
}