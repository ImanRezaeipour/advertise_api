using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Factory.Companies
{
    public interface ICompanyTagFactory
    {
        Task<CompanyTagListModel> PrepareListModelAsync(CompanyTagSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}