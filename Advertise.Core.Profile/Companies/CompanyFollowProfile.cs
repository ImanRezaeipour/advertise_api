using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanyFollowProfile : BaseProfile
    {
        public CompanyFollowProfile()
        {
            CreateMap<CompanyFollow, CompanyFollowCreateModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyFollow, CompanyFollowEditModel>(MemberList.None)
                .ReverseMap()
                .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
                .ForMember(dest => dest.FollowedById, opt => opt.Ignore());

            CreateMap< CompanyFollowEditModel, CompanyFollow>(MemberList.None).ReverseMap();

            CreateMap<CompanyFollow, CompanyFollowListModel>(MemberList.None).ReverseMap();

            CreateMap<CompanyFollowModel, CompanyFollow>(MemberList.None).ReverseMap()
                .ForMember(dest => dest.AvatarFileName, opt => opt.MapFrom(surc => surc.FollowedBy.Meta.AvatarFileName))
                .ForMember(dest => dest.FollowedById, opt => opt.MapFrom(surc => surc.FollowedById))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(surc => surc.FollowedBy.Meta.FullName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(surc => surc.FollowedBy.Meta.PhoneNumber))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(surc => surc.FollowedBy.UserName));
        }
    }
}