using System.Collections.Generic;
using Advertise.Core.Model.Categories;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.General
{
    public class MainMenuModel : BaseModel
    {
        public IEnumerable<CategoryModel> Categories { get; set; }
    }
}