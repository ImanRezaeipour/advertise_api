using Advertise.Core.Domain.Announces;
using Advertise.Core.Model.Announces;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Announces
{
    public class AnnounceReserveProfile : BaseProfile
    {
        public AnnounceReserveProfile()
        {
            CreateMap<AnnounceReserve, AnnounceReserveCreateModel>(MemberList.None).ReverseMap();
        }
    }
}