using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Seos;

namespace Advertise.Service.Seos
{
    public interface ISeoService
    {
        Task CreateByViewModelAsync(SeoCreateModel model);
        Task DeleteByIdAsync(Guid seoId);
        Task<bool> IsExistCategoryByIdAsync(string categoryId);
    }
}