using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Products;
using Advertise.Core.Types;

namespace Advertise.Service.Products
{
    public interface IProductCommentService
    {
        Task EditApproveByViewModelAsync(ProductCommentEditModel model);
        Task<int> CountByRequestAsync(ProductCommentSearchModel model);
        Task CreateByViewModelAsync(ProductCommentCreateModel model);
        Task DeleteByIdAsync(Guid productCommentId, bool isCurrentUser = false);
        Task EditByViewModelAsync(ProductCommentEditModel model, bool isCurrentUser = false);
        Task<ProductComment> FindByIdAsync(Guid productCommentId);
        Task<IList<ProductComment>> GetByRequestAsync(ProductCommentSearchModel model);
        Task<ProductCommentListModel> ListByRequestAsync(ProductCommentSearchModel model);
        IQueryable<ProductComment> QueryByRequest(ProductCommentSearchModel model);
        Task EditRejectByViewModelAsync(ProductCommentEditModel model);
        Task RemoveRangeAsync(IList<ProductComment> productComments);
        Task SetStateByIdAsync(Guid productCommentId, StateType state);
    }
}