namespace Advertise.Core.Managers.Event
{
    public interface IEventPublisher
    {
        #region Public Methods

        void Publish<T>(T eventMessage);

        #endregion Public Methods
    }
}