using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Categories;

namespace Advertise.Service.Factory.Categories
{
    public interface ICategoryFollowFactory
    {
        Task<CategoryFollowListModel> PrepareListModelAsync(bool isCurrentUser = false, Guid? userId = null);
    }
}