using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Catalogs;
using Advertise.Core.Model.Products;
using Advertise.Core.Model.Tags;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Products
{
    public class ProductProfile : BaseProfile
    {
        public ProductProfile()
        {
            CreateMap<ProductCreateModel, Product>(MemberList.None)
                .ForMember(dest => dest.ProductKeywords, opt => opt.Ignore());

            CreateMap<Product, ProductCreateModel>(MemberList.None)
                .ForMember(dest => dest.MetaTitle, opts => opts.MapFrom(src => src.Title))
                .ForMember(dest => dest.MetaDescription, opts => opts.MapFrom(src => src.Description));

            CreateMap<ProductEditModel, Product>(MemberList.None)
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Code, opt => opt.Ignore())
                .ForMember(dest => dest.Features, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore())
                .ForMember(dest => dest.Specifications, opt => opt.Ignore())
                .ForMember(dest => dest.ProductKeywords, opt => opt.Ignore());

            CreateMap<Product, ProductEditModel>(MemberList.None)
                .ForMember(dest => dest.CategoryTitle, opts => opts.MapFrom(src => src.Category.Title))
                .ForMember(dest => dest.Features, opts => opts.MapFrom(src => src.Features))
                .ForMember(dest => dest.Code, opts => opts.Ignore())
                .ForMember(dest => dest.Images, opts => opts.MapFrom(src => src.Images));

            CreateMap<Product, ProductModel>(MemberList.None)
                .ForMember(dest => dest.DiscountPercent, opts => opts.MapFrom(src => src.Price/*.GetDiscount(src.PreviousPrice)*/))
                .ForMember(dest => dest.CategoryTitle, opts => opts.MapFrom(src => src.Category.Title))
                .ForMember(dest => dest.CompanyTitle, opts => opts.MapFrom(src => src.Company.Title))
                .ForMember(dest => dest.CompanyLogoFileName, opts => opts.MapFrom(src => src.Company.LogoFileName))
                .ForMember(dest => dest.CompanyId, opts => opts.MapFrom(src => src.Company.Id))
                .ForMember(dest => dest.CatAlias, opts => opts.MapFrom(src => src.Category.Alias))
                .ForMember(dest => dest.CatMetaTitle, opts => opts.MapFrom(src => src.Category.MetaTitle))
                .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.IsCatalog == true ? src.Catalog.Title : src.Title))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.IsCatalog == true ? src.Catalog.Description : src.Description))
                .ForMember(dest => dest.ImageFileName, opts => opts.MapFrom(src => src.IsCatalog == true ? src.Catalog.ImageFileName : src.ImageFileName))
                .ForMember(dest => dest.CategoryId, opts => opts.MapFrom(src => src.IsCatalog == true ? src.Catalog.CategoryId : src.CategoryId))
                .ForMember(dest => dest.ManufacturerId, opts => opts.MapFrom(src => src.IsCatalog == true ? src.Catalog.ManufacturerId : src.ManufacturerId))
                .ForMember(dest => dest.Code, opts => opts.MapFrom(src => src.IsCatalog == true ? src.Catalog.Code : src.Code));

            CreateMap<ProductModel, Product>(MemberList.None);

            CreateMap<Product, ProductDetailModel>(MemberList.None)
                .ForMember(dest => dest.CategoryTitle, opts => opts.MapFrom(src => src.Category.Title))
                .ForMember(dest => dest.CompanyId, opts => opts.MapFrom(src => src.Company.Id))
                .ForMember(dest => dest.CompanyTitle, opts => opts.MapFrom(src => src.Company.Title))
                .ForMember(dest => dest.CompanyLogoFileName, opts => opts.MapFrom(src => src.Company.LogoFileName))
                .ForMember(dest => dest.CompanyDescription, opts => opts.MapFrom(src => src.Company.Description))
                .ForMember(dest => dest.CompanySlogan, opts => opts.MapFrom(src => src.Company.Slogan))
                .ForMember(dest => dest.DiscountPercent,opts => opts.MapFrom(src => src.Price/*.GetDiscount(src.PreviousPrice)*/))
                .ForMember(dest => dest.HaveDiscount, opts => opts.MapFrom(src => src.PreviousPrice > src.Price))
                .ForMember(dest => dest.Title,opts => opts.MapFrom(src => src.IsCatalog == true ? src.Catalog.Title : src.Title))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.IsCatalog == true ? src.Catalog.Description : src.Description))
                .ForMember(dest => dest.ImageFileName, opts => opts.MapFrom(src => src.IsCatalog == true ? src.Catalog.ImageFileName : src.ImageFileName))
                .ForMember(dest => dest.CategoryId, opts => opts.MapFrom(src => src.IsCatalog == true ? src.Catalog.CategoryId : src.CategoryId))
                .ForMember(dest => dest.ManufacturerId, opts => opts.MapFrom(src => src.IsCatalog == true ? src.Catalog.ManufacturerId : src.ManufacturerId))
                .ForMember(dest => dest.GuaranteeTitle, opts => opts.MapFrom(src => src.Guarantee.Title));

            CreateMap<ProductDetailModel, Product>(MemberList.None);

            CreateMap<Product, TagModel>(MemberList.None).ReverseMap();

            CreateMap<Product, ProductBulkModel>(MemberList.None);

            CreateMap<ProductBulkModel, Product>(MemberList.None)
                .ForMember(dest => dest.GuaranteeId, opt => opt.Ignore());

            CreateMap<Product, ProductEditCatalogModel>(MemberList.None);

            CreateMap<ProductEditCatalogModel, Product>(MemberList.None)
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
                .ForMember(dest => dest.CatalogId, opt => opt.Ignore())
                .ForMember(dest => dest.GuaranteeId, opt => opt.Ignore());

            CreateMap<Product, CatalogDetailModel>(MemberList.None)
                .ForMember(dest => dest.CompanyId, opts => opts.MapFrom(src => src.Company.Id))
                .ForMember(dest => dest.ProductId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.CompanyAlias, opts => opts.MapFrom(src => src.Company.Alias))
                .ForMember(dest => dest.CompanyTitle, opts => opts.MapFrom(src => src.Company.Title))
                .ForMember(dest => dest.CompanyLogoFileName, opts => opts.MapFrom(src => src.Company.LogoFileName))
                .ForMember(dest => dest.ProductModifiedOn, opts => opts.MapFrom(src => src.ModifiedOn))
                .ForMember(dest => dest.ManufacturerId, opts => opts.MapFrom(src => src.IsCatalog == true ? src.Catalog.ManufacturerId : src.ManufacturerId))
                .ForMember(dest => dest.ProductSell, opts => opts.MapFrom(src => src.Sell))
                .ForMember(dest => dest.ProductColor, opts => opts.MapFrom(src => src.Color.Value))
                .ForMember(dest => dest.ProductGuaranteeTitle, opts => opts.MapFrom(src => src.Guarantee.Title))
                .ForMember(dest => dest.ProductPrice, opts => opts.MapFrom(src => src.Price));

            CreateMap<CatalogDetailModel, Product>(MemberList.None);
        }
    }
}