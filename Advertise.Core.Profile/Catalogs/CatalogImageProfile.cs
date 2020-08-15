using Advertise.Core.Domain.Catalogs;
using Advertise.Core.Model.Catalogs;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Catalogs
{
    public class CatalogImageProfile : BaseProfile
    {
        public CatalogImageProfile()
        {
            CreateMap<CatalogImage, CatalogImageModel>(MemberList.None).ReverseMap();
        }
    }
}