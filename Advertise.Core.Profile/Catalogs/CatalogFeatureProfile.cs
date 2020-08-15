using Advertise.Core.Domain.Catalogs;
using Advertise.Core.Model.Catalogs;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Catalogs
{
    public class CatalogFeatureProfile : BaseProfile
    {
        public CatalogFeatureProfile()
        {
            CreateMap<CatalogFeature, CatalogFeatureModel>(MemberList.None).ReverseMap();
        }
    }
}