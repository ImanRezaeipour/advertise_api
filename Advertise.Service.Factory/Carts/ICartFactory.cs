using System;
using System.Threading.Tasks;
using Advertise.Core.Model.Carts;

namespace Advertise.Service.Factory.Carts
{
    public interface ICartFactory
    {
        Task<CartDetailModel> PrepareDetailModelAsync();
        Task<CartListModel> PrepareListModelAsync(CartSearchModel request, bool isCurrentUser = false, Guid? userId = null);
        Task<CartInfoModel> PrepareInfoModelAsync();
    }
}