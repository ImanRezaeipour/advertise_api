using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Catalogs;

namespace Advertise.Service.Factory.Catalogs
{
    public interface ICatalogFactory
    {
        Task<CatalogCreateModel> PrepareCreateModelAsync(CatalogCreateModel viewModelPrepare = null);
        Task<CatalogEditModel> PrepareEditModelAsync(Guid catalogId);
        Task<CatalogListModel> PrepareListModelAsync(CatalogSearchModel request, bool isCurrentUser = false, Guid? userId = null);
    }
}