using Advertise.Core.Domain.Products;
using Advertise.Core.Model.Products;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Products
{
    public class ProductNotifyProfile : BaseProfile
    {
        public ProductNotifyProfile()
        {
            CreateMap<ProductNotify, ProductNotifyModel>(MemberList.None).ReverseMap();
        }
    }
}