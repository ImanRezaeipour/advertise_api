using Advertise.Core.Model.Categories;
using Advertise.Core.Model.Common;
using Advertise.Core.Types;

namespace Advertise.Core.Model.General
{
    public class ProfileMenuModel : BaseModel
    {
        public string Alias { get; set; }
        public CategoryOptionModel CategoryOption { get; set; }
        public StateType State { get; set; }
    }
}