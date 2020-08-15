using System;
using System.Threading;
using StructureMap;

namespace Advertise.Core.Infrastructure.DependencyManagement
{
    public static class ContainerManager
    {
        #region Private Fields

        private static readonly Lazy<Container> ContainerBuilder = new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        #endregion Private Fields

        #region Public Properties

        public static IContainer Container => ContainerBuilder.Value;

        #endregion Public Properties

        #region Private Methods

        private static Container DefaultContainer()
        {
            var container = new Container();
            
            return container;
        }

        #endregion Private Methods
    }
}