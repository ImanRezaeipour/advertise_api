using Advertise.Core.Domain.Users;
using Advertise.Core.Model.General;
using Advertise.Core.Model.Users;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Users
{
    public class UserProfile : BaseProfile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>(MemberList.None).ReverseMap();

            CreateMap<User, ProfileModel>(MemberList.None).ReverseMap();

            CreateMap<User, UserCreateModel>(MemberList.None).ReverseMap();
            
            CreateMap<UserMeta, UserCreateModel>(MemberList.None).ReverseMap();
            
            CreateMap<User, UserOperatorCreateModel>(MemberList.None).ReverseMap();

            CreateMap<User, UserEditModel>(MemberList.None);

            CreateMap<UserMeta, ProfileHeaderModel>(MemberList.None).ReverseMap();

            CreateMap<UserMeta, DashboardHeaderModel>(MemberList.None).ReverseMap();

            CreateMap<UserMeta, UserEditMeModel>(MemberList.None).ReverseMap();

            CreateMap<UserEditModel, UserEditMeModel>(MemberList.None).ReverseMap();

            CreateMap<User, UserEditMeModel>(MemberList.None)
                .ForMember(dest => dest.AvatarFileName, opt => opt.MapFrom(src => src.Meta.AvatarFileName))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Meta.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Meta.LastName))
                .ForMember(dest => dest.HomeNumber, opt => opt.MapFrom(src => src.Meta.HomeNumber))
                .ForMember(dest => dest.NationalCode, opt => opt.MapFrom(src => src.Meta.NationalCode))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Meta.Location))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Meta.Gender));

            CreateMap<UserEditMeModel, UserMeta>(MemberList.None);

            CreateMap<UserEditMeModel, UserEditModel>(MemberList.None);

            CreateMap<UserEditModel, User>(MemberList.None)
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.Ignore());

            CreateMap<UserEditMeModel, User>(MemberList.None)
                .ForMember(dest => dest.Email, opt => opt.Ignore());

            CreateMap<User, UserDetailModel>(MemberList.None).ReverseMap();

            CreateMap<UserMeta, UserDetailModel>(MemberList.None).ReverseMap();

            CreateMap<User, UserSearchModel>(MemberList.None).ReverseMap();

            CreateMap<User, UserListModel>(MemberList.None).ReverseMap();

            CreateMap<User, UserRegisterModel>(MemberList.None).ReverseMap();
            
            CreateMap<User, UserAddPhoneNumberModel>(MemberList.None).ReverseMap();

            CreateMap<UserMeta, ProfileHeaderModel>(MemberList.None).ReverseMap();

            CreateMap<UserMeta, UserHeaderModel>(MemberList.None).ReverseMap();
        }
    }
}