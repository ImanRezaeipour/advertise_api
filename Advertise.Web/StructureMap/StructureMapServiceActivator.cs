using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Advertise.Core.Infrastructure.DependencyManagement;

namespace Advertise.Web.StructureMap
{
    public class StructureMapServiceActivator : IHttpControllerActivator
    {
        public StructureMapServiceActivator(HttpConfiguration configuration) {}    

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = ContainerManager.Container.GetInstance(controllerType) as IHttpController;
            return controller;
        }
    }
}