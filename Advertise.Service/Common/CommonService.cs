using System;
using System.Threading.Tasks;
using Advertise.Core.Managers.WebContext;

namespace Advertise.Service.Common
{
    public class CommonService : ICommonService
    {
        #region Private Fields

        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CommonService(IWebContextManager webContextManager)
        {
            _webContextManager = webContextManager;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Guid?> GetUserIdAsync(bool isCurrentUser, Guid? userId)
        {
            if (isCurrentUser)
                return _webContextManager.CurrentUserId;

            return userId;
        }

        public int RandomNumberByCount(int min, int max)
        {
            var random = new Random().Next(min, max);
            return random;
        }
       
        #endregion Public Methods
    }
}