using Advertise.Core.Domain.Locations;
using Advertise.Core.Model.Locations;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Locations
{
    public class LocationCityProfile : BaseProfile
    {
        public LocationCityProfile()
        {
            CreateMap<LocationCity, LocationCityModel>(MemberList.None);

            CreateMap<LocationCityModel, LocationCity>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}