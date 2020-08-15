using Advertise.Core.Domain.Receipts;
using Advertise.Core.Model.Carts;
using Advertise.Core.Model.Receipts;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Receipts
{
    public class ReceiptOptionProfile : BaseProfile
    {
        public ReceiptOptionProfile()
        {
            CreateMap<ReceiptOption, ReceiptOptionCreateModel>(MemberList.None).ReverseMap();

            CreateMap<ReceiptOption, ReceiptOptionListModel>(MemberList.None).ReverseMap();

            CreateMap<ReceiptOption, ReceiptOptionModel>(MemberList.None).ReverseMap();

            CreateMap<ReceiptOption, ReceiptOptionEditModel>(MemberList.None).ReverseMap();

            CreateMap<ReceiptOption, ReceiptOptionDetailModel>(MemberList.None).ReverseMap();

            CreateMap<ReceiptOption, ReceiptOptionSearchModel>(MemberList.None).ReverseMap();

            CreateMap<ReceiptOption, CartListModel>(MemberList.None).ReverseMap();
        }
    }
}