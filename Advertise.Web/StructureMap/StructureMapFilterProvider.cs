using System.Collections.Generic;
using System.Web.Mvc;
using Advertise.Core.Infrastructure.DependencyManagement;
using StructureMap;
using StructureMap.Attributes;

namespace Advertise.Web.StructureMap
{
    public class StructureMapFilterProvider : FilterAttributeFilterProvider
    {
        private IContainer _container;

        [SetterProperty]
        public IContainer Container
        {
            set { _container = value; }
        }

        //public StructureMapFilterProvider(IContainer container)
        //{
        //    _container = container;
        //}

        public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(controllerContext, actionDescriptor);
            foreach (var filter in filters)
            {
                ContainerManager.Container.BuildUp(filter.Instance);
                yield return filter;
            }
        }
    }
}