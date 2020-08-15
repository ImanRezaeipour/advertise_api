using Advertise.Core.Domain.Carts;
using Advertise.Core.Domain.Receipts;
using Advertise.Core.Model.Carts;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Carts
{
    public class CartProfile : BaseProfile
    {
        public CartProfile()
        {
            CreateMap<CartModel, Cart>(MemberList.None);

            CreateMap<Cart, CartModel>(MemberList.None)
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.ProductTitle, opt => opt.MapFrom(src => src.Product.Title))
                .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Product.Description))
                .ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.Product.Code))
                .ForMember(dest => dest.ProductImageFileName, opt => opt.MapFrom(src => src.Product.ImageFileName))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Product.Company.Id))
                .ForMember(dest => dest.CompanyTitle, opt => opt.MapFrom(src => src.Product.Company.Title))
                .ForMember(dest => dest.CompanyAlias, opt => opt.MapFrom(src => src.Product.Company.Alias))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Product.Category.Id))
                .ForMember(dest => dest.CategoryTitle, opt => opt.MapFrom(src => src.Product.Category.Title))
                .ForMember(dest => dest.TotalProductPrice, opt => opt.MapFrom(src => src.Product.Price*src.ProductCount));

            CreateMap<Cart, CartUpdateInfoModel>(MemberList.None).ReverseMap();

            CreateMap<Cart, CartListModel>(MemberList.None).ReverseMap();

            CreateMap<Cart, CartDetailModel>(MemberList.None).ReverseMap();

            CreateMap<Cart, CartInfoModel>(MemberList.None)
                .ForMember(dest=>dest.UserFirstName, opt=>opt.MapFrom(src=> src.CreatedBy.Meta.FirstName))
                .ForMember(dest=>dest.UserLastName, opt=>opt.MapFrom(src=> src.CreatedBy.Meta.LastName))
                .ForMember(dest=>dest.UserNationalCode, opt=>opt.MapFrom(src=> src.CreatedBy.Meta.NationalCode))
                .ForMember(dest=>dest.UserAddress, opt=>opt.MapFrom(src=> src.CreatedBy.Meta.Location.Extra))
                .ForMember(dest=>dest.PostalCode, opt=>opt.MapFrom(src=> src.CreatedBy.Meta.Location.PostalCode))
                .ForMember(dest=>dest.PhoneNumber, opt=>opt.MapFrom(src=> src.CreatedBy.PhoneNumber))
                .ForMember(dest=>dest.MobileNumber, opt=>opt.MapFrom(src=> src.CreatedBy.Meta.HomeNumber));

            CreateMap<CartInfoModel, Cart>(MemberList.None);

            CreateMap<CartListModel,ReceiptOption >(MemberList.None).ReverseMap();
        }
    }
}