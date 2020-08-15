using Advertise.Core.Domain.Catalogs;
using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Products;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Products
{
    public class ProductFeatureProfile : BaseProfile
    {
        public ProductFeatureProfile()
        {
            CreateMap< ProductFeatureEditModel, ProductFeature>(MemberList.None).ReverseMap();

            CreateMap<ProductFeature, ProductFeatureEditModel>(MemberList.None)
                .ForMember(dest => dest.ProductId, opt => opt.Ignore());

            CreateMap<ProductFeature, ProductFeatureCreateModel>(MemberList.None).ReverseMap();

            CreateMap<ProductFeature, ProductFeatureModel>(MemberList.None).ReverseMap();

            CreateMap<ProductFeature, ProductFeatureDetailModel>(MemberList.None).ReverseMap();

            CreateMap<CatalogFeature, ProductFeatureModel>(MemberList.None).ReverseMap();
        }
    }
}