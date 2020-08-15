using Advertise.Core.Domain.Announces;
using Advertise.Core.Model.Announces;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Announces
{
   public class AnnounceProfile : BaseProfile
    {
        public AnnounceProfile()
        {
            CreateMap<Announce, AnnounceCreateModel>(MemberList.None).ReverseMap();
            
            CreateMap<AnnounceEditModel, Announce>(MemberList.None)
                .ForMember(dest => dest.AnnounceOptionId, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
                .ForMember(dest => dest.EntityId, opt => opt.Ignore())
                .ForMember(dest => dest.FinalPrice, opt => opt.Ignore())
                .ForMember(dest => dest.TargetId, opt => opt.Ignore());
            
            CreateMap<Announce, AnnounceEditModel>(MemberList.None)
                .ForMember(dest => dest.AdsOptionName,opt =>opt.MapFrom(src => src.AnnounceOption.Title))
                .ForMember(dest => dest.Duration,opt =>opt.MapFrom(src => src.DurationType.Value));
        }
    }
}