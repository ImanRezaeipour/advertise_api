using System;
using System.Threading.Tasks;
using Advertise.Core.Domain.Categories;
using Advertise.Core.Events;
using Advertise.Core.Managers.Event;

namespace Advertise.Service.Event.Cache
{
    public class CacheEvents : IEventHandler<EntityInserted<Category>>
    {
        public async Task HandleEvent(EntityInserted<Category> eventMessage)
        {
            throw new NotImplementedException();
        }
    }
}