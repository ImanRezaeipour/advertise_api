using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Guarantees
{
    public class GuaranteeCreateModel:BaseModel
    {
        public string Description { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Title { get; set; }
    }
}
