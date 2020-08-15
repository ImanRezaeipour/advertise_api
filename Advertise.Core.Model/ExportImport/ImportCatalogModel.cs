using System;
using System.Web;
using Advertise.Core.Model.Common;

namespace Advertise.Core.Model.ExportImport
{
    public class ImportCatalogModel : BaseModel
    {
        public Guid CategoryId { get; set; }
        public HttpPostedFileBase CatalogFile { get; set; }
    }
}