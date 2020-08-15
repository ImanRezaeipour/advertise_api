using System;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanySlideModel : BaseModel
    {
        public Guid? Id { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? ProductId { get; set; }
        public string ImageFileName { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
    }
}