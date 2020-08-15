using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Products;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Products
{
    public class ProductRateProfile : BaseProfile
    {
        public ProductRateProfile()
        {
            CreateMap<ProductRate, ProductRateModel>(MemberList.None).ReverseMap();

            CreateMap<ProductRate, ProductRateCreateModel>(MemberList.None).ReverseMap();
        }
    }
}