using Advertise.Core.Domain.Manufacturers;
using Advertise.Core.Model.Manufacturers;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Manufacturers
{
    public class ManufacturerProfile : BaseProfile
    {
        public ManufacturerProfile()
        {
            CreateMap<Manufacturer, ManufacturerEditModel>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<Manufacturer, ManufacturerCreateModel>(MemberList.None).ReverseMap();
            
            CreateMap<Manufacturer, ManufacturerListModel>(MemberList.None).ReverseMap();
            
            CreateMap<Manufacturer, ManufacturerModel>(MemberList.None).ReverseMap();
        }
    }
}
