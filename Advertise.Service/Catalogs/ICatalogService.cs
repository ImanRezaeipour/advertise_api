using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Catalogs;
using Advertise.Core.Model.Catalogs;
using Advertise.Core.Model.Common;
using Advertise.Core.Objects;

namespace Advertise.Service.Catalogs
{
    public interface ICatalogService
    {
        Task<Catalog> FindByIdAsync(Guid catalogId);
        Task DeleteByIdAsync(Guid catalogId);
        Task EditByViewModelAsync(CatalogEditModel viewModel);
        Task CreateByViewModelAsync(CatalogExportModel viewModel);
        Task<int> CountByRequestAsync(CatalogSearchModel request);
        Task<IList<Catalog>> GetByRequestAsync(CatalogSearchModel request);
        IQueryable<Catalog> QueryByRequest(CatalogSearchModel request);
        Task CreateByViewModelAsync(CatalogCreateModel viewModel);
        Task<IList<FineUploaderObject>> GetFilesAsFineUploaderModelByIdAsync(Guid catalogId);
        Task<IList<FineUploaderObject>> GetFileAsFineUploaderModelByIdAsync(Guid catalogId);
        Task<IList<SelectList>> GetAsSelectListAsync();
        Task<IList<Select2Object>> GetAsSelect2ObjectAsync();
        Task ImportCatalogsFromXlsxAsync(Stream stream, Guid categoryId);
        Task<byte[]> ExportCatalogsToXlsxAsync(IEnumerable<CatalogExportModel> catalogs, Guid categoryId);
        Task<byte[]> GetCatalogTemplateAsExcelAsync(Guid categoryId);
    }
}