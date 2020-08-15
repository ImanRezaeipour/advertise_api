using Advertise.Core.Domain.Users;
using Advertise.Core.Model.Users;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Users
{
    public class UserViolationProfile : BaseProfile
    {
        public UserViolationProfile()
        {
            CreateMap<UserViolation, UserViolationModel>(MemberList.None).ReverseMap();

            CreateMap<UserViolation, UserViolationCreateModel>(MemberList.None).ReverseMap();

            CreateMap<UserViolation, UserViolationEditModel>(MemberList.None).ReverseMap();

            CreateMap<UserViolation, UserViolationListModel>(MemberList.None).ReverseMap();

            CreateMap<UserViolation, UserViolationDetailModel>(MemberList.None).ReverseMap();
        }
    }
}