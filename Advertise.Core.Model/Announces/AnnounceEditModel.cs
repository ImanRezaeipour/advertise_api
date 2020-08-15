using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Announces
{
    public class AnnounceEditModel : BaseModel
    {
        public Guid Id { get; set; }
        public string Duration { get; set; }
        public string AdsOptionName { get; set; }
        public decimal? FinalPrice { get; set; }
        public int? Order { get; set; }
        public string ImageFileName { get; set; }
        public string Title { get; set; }
    }
}