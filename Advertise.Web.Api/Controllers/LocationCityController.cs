using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advertise.Core.Model.Common;
using Advertise.Service.Locations;

namespace Advertise.Web.Api.Controllers
{
    public class LocationCityController : BaseController
    {
        #region Private Fields

        private readonly ILocationCityService _cityService;

        #endregion Private Fields

        #region Public Constructors

        public LocationCityController(ILocationCityService cityService)
        {
            _cityService = cityService;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> GetCityAjax(RequestModel model)
        {
            var cities = await _cityService.GetCityAsSelectListItemAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, cities);
        }

        public virtual async Task<HttpResponseMessage> GetStateAjax()
        {
            var states = await _cityService.GetStatesAsync();
            return Request.CreateResponse(HttpStatusCode.OK, states);
        }

        public virtual async Task<HttpResponseMessage> GetLocationAjax(RequestModel model)
        {
            var city = await _cityService.GetLocationAsync(model.Id);
            return Request.CreateResponse(HttpStatusCode.OK, city);
        }

        #endregion Public Methods
    }
}