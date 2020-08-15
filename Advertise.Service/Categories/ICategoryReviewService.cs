using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Categories;
using Advertise.Core.Model.Categories;

namespace Advertise.Service.Categories
{
    public interface ICategoryReviewService
    {
        Task<int> CountByRequestAsync(CategoryReviewSearchModel request);
        Task CreateByViewModelAsync(CategoryReviewCreateModel viewModel);
        Task DeleteByIdAsync(Guid categoryReviewId);
        Task EditByViewModelAsync(CategoryReviewEditModel viewModel);
        Task<CategoryReview> FindByIdAsync(Guid categoryReviewId);
        Task<IList<CategoryReview>> GetByCategoryIdAsync(Guid categoryId);
        Task<IList<CategoryReview>> GetByRequestAsync(CategoryReviewSearchModel request);
        IQueryable<CategoryReview> QueryByRequest(CategoryReviewSearchModel request);
        Task SeedAsync();
    }
}