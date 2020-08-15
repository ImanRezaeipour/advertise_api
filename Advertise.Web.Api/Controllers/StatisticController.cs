using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Advertise.Core.Model.Statistics;
using Advertise.Service.Statistics;

namespace Advertise.Web.Api.Controllers
{

    public partial class StatisticController : BaseController
    {
        #region Private Fields

        private readonly IStatisticService _statisticService;

        #endregion Private Fields

        #region Public Constructors

        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<HttpResponseMessage> List()
        {
            var viewModel = new StatisticListViewModel();
            return Request.CreateResponse(HttpStatusCode.OK, viewModel);
        }

        #endregion Public Methods
    }
}