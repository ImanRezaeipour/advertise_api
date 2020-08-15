using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Receipts;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Receipts;
using Advertise.Core.Types;
using Advertise.Data.DbContexts;

namespace Advertise.Service.Receipts
{
    public class ReceiptOptionService : IReceiptOptionService
    {
        #region Private Fields

        private readonly IDbSet<ReceiptOption> _receiptOptionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public ReceiptOptionService(IUnitOfWork unitOfWork, IWebContextManager webContextManager)
        {
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _receiptOptionRepository = unitOfWork.Set<ReceiptOption>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(ReceiptOptionSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task<IList<ReceiptOption>> GetByRequestAsync(ReceiptOptionSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<IList<ReceiptOption>> GetMyReceiptOptionsByReceiptIdAsync(Guid receiptId, Guid? userId = null)
        {
            if (userId == null)
            {
                userId = _webContextManager.CurrentUserId;
            }
            var request = new ReceiptOptionSearchModel
            {
                PageSize = PageSize.Count100,
                SortMember = SortMember.ModifiedOn,
                UserId = userId,
                ReceiptId = receiptId
            };

            return await GetByRequestAsync(request);
        }
        
        public decimal? GetSumTotalPriceAsync(Guid userId)
        {
            return  _receiptOptionRepository
                .Where(model => model.CreatedById == userId)
                .Sum(model => model.TotalPrice);
        }

        public IQueryable<ReceiptOption> QueryByRequest(ReceiptOptionSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var receiptOptions = _receiptOptionRepository.AsNoTracking().AsQueryable();

            if (model.ListType == null && model.UserId.HasValue)
                receiptOptions = receiptOptions.Where(m => m.CreatedById == model.UserId);
            if (model.ListType == ReceiptOptionListType.Buy && model.UserId.HasValue)
                receiptOptions = receiptOptions.Where(m => m.CreatedById == model.UserId);
            if (model.ListType == ReceiptOptionListType.Sale && model.UserId.HasValue)
                receiptOptions = receiptOptions.Where(m => m.SaledById == model.UserId);
            if (model.ReceiptId.HasValue)
                receiptOptions = receiptOptions.Where(m => m.ReceiptId == model.ReceiptId);

            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;

            receiptOptions = receiptOptions.OrderBy($"{model.SortMember} {model.SortDirection}");

            return receiptOptions;
        }

        #endregion Public Methods
    }
}