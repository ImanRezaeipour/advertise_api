using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Domain.Users;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Users;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Users
{
    public class UserBudgetService : IUserBudgetService
    {
        #region Private Fields

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<UserBudget> _userBudgetRepository;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public UserBudgetService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _userBudgetRepository = unitOfWork.Set<UserBudget>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(UserBudgetSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var userBudgets = await  QueryByRequest(model).CountAsync();

            return userBudgets;
        }

        public async Task CreateByViewModelAsync(UserBudgetCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var userBudget = _mapper.Map<UserBudget>(model);
            _userBudgetRepository.Add(userBudget);

           await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid userBudgetId)
        {
            var userBudget = await _userBudgetRepository.FirstOrDefaultAsync(model => model.Id == userBudgetId);
            _userBudgetRepository.Remove(userBudget);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task EditByViewModelAsync(UserBudgetEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var userBudget = await _userBudgetRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, userBudget);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task<UserBudget> FindAsync(Guid userBudgetId)
        {
            var userBudget = await _userBudgetRepository
                .AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == userBudgetId);

            return userBudget;
        }

        public IQueryable<UserBudget> QueryByRequest(UserBudgetSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var userBudgets = _userBudgetRepository.AsNoTracking().AsQueryable();
            userBudgets = userBudgets.OrderBy($"{model.SortMember} {model.SortDirection}");

            return userBudgets;
        }

        public async Task<IList<UserBudget>> GetByRequestAsync(UserBudgetSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var userBudgets =  await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
           
            return userBudgets;
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}