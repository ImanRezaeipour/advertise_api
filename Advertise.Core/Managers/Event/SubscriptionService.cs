using System.Collections.Generic;
using System.Linq;
using Advertise.Core.Infrastructure.DependencyManagement;
using StructureMap;

namespace Advertise.Core.Managers.Event
{
    public class SubscriptionService : ISubscriptionService
    {
        #region Public Properties

        public static IContainer Container { get; set; }

        #endregion Public Properties

        #region Public Methods

        public IList<IEventHandler<T>> GetSubscriptions<T>()
        {
            //todo: handle with structuremap
            var instances = ContainerManager.Container.GetAllInstances<IEventHandler<T>>().ToList();
            return instances;
            //return EngineContext.Current.ResolveAll<IConsumer<T>>();
        }

        #endregion Public Methods
    }
}