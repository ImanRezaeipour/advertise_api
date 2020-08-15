using Advertise.Core.Domain.Catalogs;
using Advertise.Core.Model.Catalogs;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Catalogs
{
    public class CatalogProfile : BaseProfile
    {
        public CatalogProfile()
        {
            CreateMap<Catalog, CatalogCreateModel>(MemberList.None).ReverseMap();

            CreateMap<Catalog, CatalogEditModel>(MemberList.None);

            CreateMap<CatalogEditModel, Catalog>(MemberList.None)
                .ForMember(dest => dest.Code, opt => opt.Ignore());

            CreateMap<Catalog, CatalogExportModel>(MemberList.None).ReverseMap();

            CreateMap<CatalogCreateModel, CatalogExportModel>(MemberList.None).ReverseMap();

            CreateMap<CatalogEditModel, CatalogExportModel>(MemberList.None).ReverseMap();

            CreateMap<Catalog, CatalogModel>(MemberList.None).ReverseMap();
        }
    }
}