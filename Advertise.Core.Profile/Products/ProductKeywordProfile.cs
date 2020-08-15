using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Products;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Products
{
    public class ProductKeywordProfile : BaseProfile
    {
        public ProductKeywordProfile()
        {
            CreateMap<ProductKeyword, ProductKeywordModel>(MemberList.None).ReverseMap();

            CreateMap<ProductKeyword, ProductKeywordCreateModel>(MemberList.None).ReverseMap();

            CreateMap<ProductKeyword, ProductKeywordEditModel>(MemberList.None).ReverseMap();
        }
    }
}