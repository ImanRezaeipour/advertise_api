using Advertise.Core.Domain.Users;
using Advertise.Core.Model.Users;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Users
{
    public class UserOperatorProfile :BaseProfile
    {
        public UserOperatorProfile()
        {
            CreateMap<UserOperator, UserOperatorSearchModel>(MemberList.None).ReverseMap();
            
            CreateMap<UserOperator, UserOperatorListModel>(MemberList.None).ReverseMap();
            
            CreateMap<UserOperator, UserOperatorCreateModel>(MemberList.None).ReverseMap();
            
            CreateMap<UserOperator, UserOperatorModel>(MemberList.None).ReverseMap();
        }
    }
}