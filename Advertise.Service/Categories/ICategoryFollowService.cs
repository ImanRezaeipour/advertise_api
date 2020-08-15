using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Categories;
using Advertise.Core.Domain.Users;
using Advertise.Core.Model.Categories;

namespace Advertise.Service.Categories
{
    public interface ICategoryFollowService
    {
        Task<int> CountAllFollowByCategoryIdAsync(Guid categoryId);
        Task<int> CountByCategoryIdAsync(Guid categoryId);
        Task<int> CountByRequestAsync(CategoryFollowSearchModel request);
        Task<int> CountByUserIdAsync(Guid userId);
        Task<CategoryFollow> FindByCategoryIdAsync(Guid categoryId, Guid? userId = null);
        Task<IList<CategoryFollow>> GetByRequestAsync(CategoryFollowSearchModel request);
        Task<IList<User>> GetUsersByCategoryIdAsync(Guid categoryId);
        Task<bool> IsFollowCurrentUserByCategoryIdAsync(Guid categoryId);
        Task<bool> IsFollowByCategoryIdAsync(Guid categoryId, Guid? userId = null);
        IQueryable<CategoryFollow> QueryByRequest(CategoryFollowSearchModel request);
        Task SeedAsync();
        Task ToggleFollowCurrentUserByCategoryIdAsync(Guid categoryId);
    }
}