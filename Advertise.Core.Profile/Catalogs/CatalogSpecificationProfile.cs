using Advertise.Core.Domain.Catalogs;
using Advertise.Core.Domain.Specifications;
using Advertise.Core.Model.Catalogs;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Catalogs
{
    public class CatalogSpecificationProfile : BaseProfile
    {
        public CatalogSpecificationProfile()
        {
            CreateMap<CatalogSpecification, CatalogSpecificationModel>(MemberList.None)
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Specification.Type))
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Specification.Options))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Specification.Title))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Specification.Order));

            CreateMap<CatalogSpecificationModel, CatalogSpecification>(MemberList.None);

            CreateMap<Specification, CatalogSpecificationModel>(MemberList.None)
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order));

            CreateMap<CatalogSpecificationModel, Specification>(MemberList.None);
        }
    }
}