using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Companies;

namespace Advertise.Service.Factory.Companies
{
    public interface ICompanyFollowFactory
    {
        Task<CompanyFollowListModel> PrepareListModelAsync(bool isCurrentUser = false, bool follower = false, Guid? userId = null);
    }
}