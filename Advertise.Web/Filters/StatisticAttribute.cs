using System;
using System.Web.Mvc;
using Advertise.Core.Extensions;
using Advertise.Core.Model.Statistics;
using Advertise.Core.Types;
using Advertise.Service.Statistics;
using StructureMap.Attributes;

namespace Advertise.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class StatisticAttribute:ActionFilterAttribute
    {
        private IStatisticService _statisticService;

        [SetterProperty]
        public IStatisticService StatisticService
        {
            set { _statisticService = value; }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var statisticViewmodel = new StatisticCreateModel
            {
                ControllerName = filterContext.RequestContext.RouteData.Values["controller"].ToString(),
                ActionName = filterContext.RequestContext.RouteData.Values["action"].ToString(),
                ViewedOn = DateTime.Now,
                Referrer = filterContext.RequestContext.HttpContext.Request.UrlReferrer?.ToString(),
                Audit = AuditType.Create,
                IpAddress = filterContext.HttpContext.Request.GetIp(),
                UserAgent = filterContext.HttpContext.Request.GetBrowser(),
                UserOs = filterContext.HttpContext.Request.GetOs()
            };
            _statisticService.CreateByViewModel(statisticViewmodel);

            base.OnActionExecuting(filterContext);
        }
    }
}
