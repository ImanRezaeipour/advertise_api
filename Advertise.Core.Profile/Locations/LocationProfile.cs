using Advertise.Core.Domain.Locations;
using Advertise.Core.Model.Locations;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Locations
{
    public class AddressProfile : BaseProfile
    {
        public AddressProfile()
        {
            CreateMap<Location, LocationCreateModel>(MemberList.None).ReverseMap();

            CreateMap<Location, LocationEditModel>(MemberList.None)
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<LocationEditModel, Location>(MemberList.None);

            CreateMap<Location, LocationModel>(MemberList.None);

            CreateMap<LocationModel, Location>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CityId, opt => opt.Ignore());
        }
    }
}