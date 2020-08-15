using System.Linq;
using Advertise.Core.Model.Announces;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Announces
{
    public class AnnouncePaymentProfile : BaseProfile
    {
        public AnnouncePaymentProfile()
        {
            CreateMap<Domain.Announces.AnnouncePayment, AnnouncePaymentCreateModel>(MemberList.None).ReverseMap();
            
            CreateMap<Domain.Announces.AnnouncePayment, AnnouncePaymentListModel>(MemberList.None).ReverseMap();
            
            CreateMap<Domain.Announces.AnnouncePayment, AnnouncePaymentModel>(MemberList.None)
                .ForMember(dest => dest.AdsImage, opt => opt.MapFrom(src => src.Announce.ImageFileName))
                .ForMember(dest => dest.AdsDuration,opt => opt.MapFrom(src => src.Announce.DurationType.Value))
                .ForMember(dest => dest.AdsType,opt => opt.MapFrom(src => src.Announce.AnnounceOption.Type))
                .ForMember(dest => dest.AdsPositionType,opt => opt.MapFrom(src => src.Announce.AnnounceOption.PositionType))
                .ForMember(dest => dest.StartDay, opt => opt.MapFrom(src => src.Announce.Reserves.First(item => item.AnnounceId == src.AnnounceId).Day));
        }
    }
}