using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Users;

namespace Advertise.Service.Factory.Users
{
    public interface IUserOperatorFactory
    {
        Task<UserOperatorListModel> PrepareListModel(UserOperatorSearchModel model, bool isCurrentUser = false, Guid? userId = null);
    }
}