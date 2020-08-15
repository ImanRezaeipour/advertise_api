using System;
using System.Linq;

namespace Advertise.Core.Managers.Event
{
    public class EventPublisher : IEventPublisher
    {
        #region Private Fields

        private readonly ISubscriptionService _subscriptionService;

        #endregion Private Fields

        #region Public Constructors

        public EventPublisher(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual void Publish<T>(T eventMessage)
        {
            var subscriptions = _subscriptionService.GetSubscriptions<T>();
            subscriptions.ToList().ForEach(x => PublishToConsumer(x, eventMessage));
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void PublishToConsumer<T>(IEventHandler<T> x, T eventMessage)
        {
            try
            {
                x.HandleEvent(eventMessage);
            }
            catch (Exception exc)
            {
                //log error
                //var logger = EngineContext.Current.Resolve<ILogger>();
                //we put in to nested try-catch to prevent possible cyclic (if some error occurs)
                try
                {
                    //logger.Error(exc.Message, exc);
                }
                catch (Exception)
                {
                    //do nothing
                }
            }
        }

        #endregion Protected Methods
    }
}