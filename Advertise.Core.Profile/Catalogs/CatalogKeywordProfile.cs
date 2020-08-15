using Advertise.Core.Domain.Catalogs;
using Advertise.Core.Model.Catalogs;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Catalogs
{
    public class CatalogKeywordProfile : BaseProfile
    {
        public CatalogKeywordProfile()
        {
            CreateMap<CatalogKeyword, CatalogKeywordModel>(MemberList.None).ReverseMap();
        }
    }
}