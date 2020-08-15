using System.Collections.Generic;

namespace Advertise.Core.Managers.Event
{
    public interface ISubscriptionService
    {
        #region Public Methods

        IList<IEventHandler<T>> GetSubscriptions<T>();

        #endregion Public Methods
    }
}