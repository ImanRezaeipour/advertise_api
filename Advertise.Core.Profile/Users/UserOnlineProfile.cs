using Advertise.Core.Domain.Users;
using Advertise.Core.Model.Users;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Users
{
    public class UserOnlineProfile : BaseProfile
    {
        public UserOnlineProfile()
        {
            CreateMap<UserOnline, UserOnlineModel>(MemberList.None).ReverseMap();
        }
    }
}