using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Tags;
using Advertise.Core.Model.Tags;
using Advertise.Core.Objects;

namespace Advertise.Service.Tags
{
    public interface ITagService 
    {
        Task<int> CountByRequestAsync(TagSearchModel model);
        Task CreateByViewModelAsync(TagCreateModel model);
        Task DeleteByIdAsync(Guid tagId);
        Task<Tag> FindByIdAsync(Guid tagId);
        Task<string> GenerateCodeForTagAsync();
        Task<IList<FineUploaderObject>> GetFileAsFineUploaderModelByIdAsync(Guid tagId);
        Task<string> MaxCodeByRequestAsync(TagSearchModel model, string aggregateMember);
        Task SeedAsync();
        Task EditByViewModelAsync(TagEditModel model);
        Task<IList<Tag>> GetActiveAsync();
        Task<IList<Tag>> GetByRequestAsync(TagSearchModel model);
        IQueryable<Tag> QueryByRequest(TagSearchModel model);
    }
}