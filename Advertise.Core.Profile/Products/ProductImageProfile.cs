using Advertise.Core.Domain.Catalogs;
using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Products;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Products
{
    public class ProductImageProfile : BaseProfile
    {
        public ProductImageProfile()
        {
            CreateMap<ProductImage, ProductImageModel>(MemberList.None)
                .ForMember(dest => dest.ProductTitle, opts => opts.MapFrom(src => src.Product.Title))
                .ForMember(dest => dest.ProductImageFileName, opts => opts.MapFrom(src => src.Product.ImageFileName))
                .ForMember(dest => dest.ProductCode, opts => opts.MapFrom(src => src.Product.Code))
                .ForMember(dest => dest.CompanyCode, opts => opts.MapFrom(src => src.Product.Company.Code))
                .ForMember(dest => dest.CompanyTitle, opts => opts.MapFrom(src => src.Product.Company.Title));

            CreateMap<ProductImage, ProductImageCreateModel>(MemberList.None).ReverseMap();

            CreateMap<ProductImage, ProductImageEditModel>(MemberList.None).ReverseMap();

            CreateMap<ProductImage, ProductImageDetailModel>(MemberList.None).ReverseMap();

            CreateMap<ProductImage, ProductImageModel>(MemberList.None).ReverseMap();

            CreateMap<CatalogImage, ProductImageModel>(MemberList.None).ReverseMap();
        }
    }
}