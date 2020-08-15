using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Advertise.Core.Domain.Users;
using Advertise.Data.DbContexts;
using AutoMapper;

namespace Advertise.Service.Users
{
    public class UserClaimService : IUserClaimService
    {
        #region Private Fields

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<UserClaim> _userClaimRepository;

        #endregion Private Fields

        #region Public Constructors

        public UserClaimService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userClaimRepository = unitOfWork.Set<UserClaim>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        public Task AddClaimAsync(User user, Claim claim)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Claim>> GetClaimsAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveClaimAsync(User user, Claim claim)
        {
            throw new System.NotImplementedException();
        }
    }
}