using Advertise.Core.Domain.Categories;
using Advertise.Core.Model.Categories;
using Advertise.Core.Profile.Common;

namespace Advertise.Core.Profile.Categories
{
    public static class CategoryExtension
    {
        public static TModel ToModel<TModel>(this Category entity)
        {
            return entity.MapTo<Category, TModel>();
        }

        public static TEntity ToEntity<TEntity>(this CategoryModel model)
        {
            return model.MapTo<CategoryModel, TEntity>();
        }

        public static TEntity ToEntity<TEntity>(this CategoryModel model, TEntity destination)
        {
            return model.MapTo(destination);
        }
    }
}