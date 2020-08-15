using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Advertise.Core.Domain.Users;
using Advertise.Data.DbContexts;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace Advertise.Service.Users
{
    public class UserLoginService : IUserLoginService
    {
        #region Private Fields

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<UserLogin> _userLoginRepository;

        #endregion Private Fields

        #region Public Constructors

        public UserLoginService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userLoginRepository = unitOfWork.Set<UserLogin>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        public Task AddLoginAsync(User user, UserLoginInfo login)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> FindAsync(UserLoginInfo login)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            throw new System.NotImplementedException();
        }
    }
}