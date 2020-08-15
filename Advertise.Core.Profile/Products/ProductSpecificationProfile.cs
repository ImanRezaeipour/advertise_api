using Advertise.Core.Domain.Catalogs;
using Advertise.Core.Domain.Products;
using Advertise.Core.Domain.Specifications;
using Advertise.Core.Model.Products;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Products
{
    public class ProductSpecificationProfile : BaseProfile
    {
        public ProductSpecificationProfile()
        {
            CreateMap<Specification, ProductSpecificationCreateModel>(MemberList.None).ReverseMap();

            CreateMap<Specification, ProductSpecificationEditModel>(MemberList.None).ReverseMap();

            CreateMap<ProductSpecification, ProductSpecificationEditModel>(MemberList.None).ReverseMap();

            CreateMap<ProductSpecification, ProductSpecificationCreateModel>(MemberList.None).ReverseMap();

            CreateMap<ProductSpecification, ProductSpecificationDetailModel>(MemberList.None).ReverseMap();

            CreateMap<ProductSpecification, ProductSpecificationModel>(MemberList.None).ReverseMap();

            CreateMap<CatalogSpecification, ProductSpecificationModel>(MemberList.None).ReverseMap();
        }
    }
}