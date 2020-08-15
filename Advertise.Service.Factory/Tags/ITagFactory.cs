using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Tags;

namespace Advertise.Service.Factory.Tags
{
    public interface ITagFactory
    {
        Task<TagCreateModel> PrepareCreateModelAsync(TagCreateModel modelPrepare= null);
        Task<TagEditModel> PrepareEditModelAsync(Guid tagId, TagEditModel modelPrepare = null);
        Task<TagListModel> PrepareListModelAsync(TagSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}