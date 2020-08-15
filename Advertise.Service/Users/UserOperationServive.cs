using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Users;
using Advertise.Core.Exceptions;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Users;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Users
{
    public class UserOperationServive : IUserOperationServive
    {
        #region Private Fields

        private readonly IMapper _mapper;
        private readonly IWebContextManager _webContextManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<UserOperator> _userOperators;

        #endregion Private Fields

        #region Public Constructors

        public UserOperationServive(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager)
        {
            _userOperators = unitOfWork.Set<UserOperator>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequest(UserOperatorSearchModel model)
        {
            if (model == null)
                throw new ServiceException();

            return await QueryByrequest(model).CountAsync();
        }

        public async Task CreateByModelAsync(UserOperator userOperator)
        {
            if (userOperator == null)
                throw new Exception();

            userOperator.CreatedById = _webContextManager.CurrentUserId;
            _userOperators.Add(userOperator);
            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task<UserOperator> FindAsync(Guid userOperatorId)
        {
            return await _userOperators.FirstOrDefaultAsync(model => model.Id == userOperatorId);
        }

        public async Task<UserOperator> FindByUserIdAsync(Guid userId)
        {
            return await _userOperators.FirstOrDefaultAsync(model => model.CreatedById == userId);
        }

        public async Task<IList<UserOperator>> GetByRequestAsync(UserOperatorSearchModel model)
        {
            if (model == null)
                throw new ServiceException();

            return await QueryByrequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public IQueryable<UserOperator> QueryByrequest(UserOperatorSearchModel model)
        {
            if (model == null)
                throw new ServiceException();

            var userOperators = _userOperators.AsNoTracking().AsQueryable();
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;

            userOperators = userOperators.OrderBy($"{model.SortMember} {model.SortDirection}");
            return userOperators;
        }

        #endregion Public Methods
    }
}