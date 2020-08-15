using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Keywords
{
    public class KeywordCreateModel : BaseModel
    {
        public bool IsActive { get; set; }
        public string Title { get; set; }
    }
}