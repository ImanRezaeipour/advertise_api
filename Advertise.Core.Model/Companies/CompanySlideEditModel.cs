using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.Companies
{
    public class CompanySlideEditModel : BaseModel
    {
        public string Description { get; set; }
        public IEnumerable<SelectList> EntityList { get; set; }
        public Guid? Id { get; set; }
        public string ImageFileName { get; set; }
        public Guid? ProductId { get; set; }
        public string Title { get; set; }
    }
}