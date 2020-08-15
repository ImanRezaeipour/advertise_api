using Advertise.Core.Domain.Receipts;
using Advertise.Core.Model.Receipts;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Receipts
{
    public class ReceiptProfile : BaseProfile
    {
        public ReceiptProfile()
        {
            CreateMap<Receipt, ReceiptCreateModel>(MemberList.None).ReverseMap();

            CreateMap<ReceiptPayment, ReceiptPaymentCallbackModel>(MemberList.None).ReverseMap();

            CreateMap<Receipt, ReceiptListModel>(MemberList.None).ReverseMap();

            CreateMap<Receipt, ReceiptPreviewModel>(MemberList.None).ReverseMap();

            CreateMap<Receipt, ReceiptModel>(MemberList.None).ReverseMap();

            CreateMap<ReceiptModel, Receipt>(MemberList.None)
                .ForMember(dest => dest.Location, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedById, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap< ReceiptEditModel, Receipt>(MemberList.None);

            CreateMap<Receipt, ReceiptEditModel>(MemberList.None);

            CreateMap<Receipt, ReceiptDetailModel>(MemberList.None).ReverseMap();

            CreateMap<Receipt, ReceiptSearchModel>(MemberList.None).ReverseMap();
        }
    }
}