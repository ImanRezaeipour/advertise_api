using System.Threading.Tasks;
using Advertise.Core.Domain.Categories;
using Advertise.Core.Events;
using Advertise.Core.Managers.Event;
using Advertise.Core.Model.Seos;
using Advertise.Core.Types;
using Advertise.Service.Seos;

namespace Advertise.Service.Event.Seo
{
    public class SeoEvents : IEventHandler<EntityDeleted<Category>>
    {
        private readonly ISeoService _seoService;

        public SeoEvents(ISeoService seoService)
        {
            _seoService = seoService;
        }

        public async Task HandleEvent(EntityDeleted<Category> eventMessage)
        {
            var seoViewModel = new SeoCreateModel
            {
                IsActive = true,
                EntityId = eventMessage.Entity.Alias,
                EntityName = "Category",
                Language = LanguageType.Persian,
                Slug = eventMessage.Entity.MetaTitle
            };
            await _seoService.CreateByViewModelAsync(seoViewModel);
        }
    }
}