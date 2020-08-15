using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanySocialModel : BaseModel
    {
        public string CompanyTitle { get; set; }
        public string FacebookLink { get; set; }
        public string GooglePlusLink { get; set; }
        public Guid Id { get; set; }
        public string InstagramLink { get; set; }
        public string TelegramLink { get; set; }
        public string TwitterLink { get; set; }
        public string YoutubeLink { get; set; }
    }
}