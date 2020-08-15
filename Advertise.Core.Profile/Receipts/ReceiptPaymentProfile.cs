using Advertise.Core.Domain.Receipts;
using Advertise.Core.Model.Receipts;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Receipts
{
    public class ReceiptPaymentProfile : BaseProfile
    {
        public ReceiptPaymentProfile()
        {
            CreateMap<ReceiptPayment, ReceiptPaymentListModel>(MemberList.None).ReverseMap();

            CreateMap<ReceiptPayment, ReceiptPaymentModel>(MemberList.None).ReverseMap();
        }
    }
}