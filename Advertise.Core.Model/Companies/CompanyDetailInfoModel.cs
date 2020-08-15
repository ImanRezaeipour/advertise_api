using Advertise.Core.Model.Common;
using Advertise.Core.Model.Locations;

namespace Advertise.Core.Model.Companies
{
    public class CompanyDetailInfoModel : BaseModel
    {
        public LocationModel Location { get; set; }
        public string Email { get; set; }
        public string FacebookLink { get; set; }
        public string GooglePlusLink { get; set; }
        public string InstagramLink { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string TelegramLink { get; set; }
        public string TwitterLink { get; set; }
        public string WebSite { get; set; }
        public string YoutubeLink { get; set; }
    }
}