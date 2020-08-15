using System.Threading.Tasks;

namespace Advertise.Core.Managers.Event
{
    public interface IEventHandler<T>
    {
        #region Public Methods

        Task HandleEvent(T eventMessage);

        #endregion Public Methods
    }
}