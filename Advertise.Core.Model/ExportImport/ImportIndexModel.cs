using System;
using System.Collections.Generic;
using Advertise.Core.Common;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.ExportImport
{
    public class ImportIndexModel : BaseModel
    {
        public IEnumerable<SelectList> CategoryList { get; set; }
        public Guid CategoryId { get; set; }
    }
}