using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Categories;

namespace Advertise.Service.Factory.Categories
{
    public interface ICategoryReviewFactory
    {
        Task<CategoryReviewDetailModel> PrepareDetailModelAsync(Guid categoryReviewId);
        Task<CategoryReviewEditModel> PrepareEditModelAsync(Guid categoryReviewId);
        Task<CategoryReviewListModel> PrepareListModelAsync(CategoryReviewSearchModel request, bool isCurrentUser = false, Guid? userId = null);
    }
}