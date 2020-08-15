using System.Threading.Tasks;
using Advertise.Core.Domain.Products;
using Advertise.Core.Events;
using Advertise.Core.Managers.Event;
using Advertise.Service.Users;

namespace Advertise.Service.Event.Notification
{
    public class NotificationEvents : IEventHandler<EntityUpdated<Product>>
    {
        private readonly IUserNotificationService _notificationService;

        public NotificationEvents(IUserNotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task HandleEvent(EntityUpdated<Product> eventMessage)
        {
            await _notificationService.CreateAsync(eventMessage.Entity.Id);
        }
    }
}