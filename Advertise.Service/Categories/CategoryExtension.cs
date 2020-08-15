using System.Collections.Generic;
using System.Linq;
using Advertise.Core.Domain.Categories;
using Advertise.Core.Model.Categories;

namespace Advertise.Service.Categories
{
    public static class CategoryExtension
    {
        #region Public Methods

        public static IEnumerable<CategoryModel> GetSubLevelOneByAlias(this IEnumerable<CategoryModel> categories, string alias)
        {
            var category = categories.FirstOrDefault(model => model.Alias == alias);
            if (category != null)
            {
                var subCategories = categories.Where(model => model.ParentId == category.Id).OrderBy(model => model.Order).ToList();
                return subCategories;
            }
            return null;
        }

        public static IEnumerable<Category> GetAllParentsByIdAsync(this IEnumerable<Category> categoryList, Category category)
        {
            var parent = categoryList.FirstOrDefault(model => model.Id == category.ParentId);

            if (parent == null)
                return Enumerable.Empty<Category>();

            return new[] { parent }.Concat(GetAllParentsByIdAsync(categoryList, parent));
        }

        public static IEnumerable<Category> GetAllChildsById(this IList<Category> categoryList, Category category)
        {
            var childs = categoryList.Where(model => model.ParentId == category.Id).ToList();

            if (childs.Count <= 0)
                return Enumerable.Empty<Category>();

            var childList = new List<Category>();
            foreach (var child in childs)
            {
                childList.AddRange(new[] { child }.Concat(GetAllChildsById(categoryList, child)));
            }

            return childList;
        }

        #endregion Public Methods
    }
}