using Advertise.Core.Domain.Plans;
using Advertise.Core.Model.Plans;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Plans
{
    public class PlanPaymentProfile : BaseProfile
    {
        public PlanPaymentProfile()
        {
            CreateMap<PlanPyamentCreateModel, PlanPayment>(MemberList.None).ReverseMap();

            CreateMap<PlanPaymentCallbackModel, PlanPayment>(MemberList.None).ReverseMap();

            CreateMap<PlanPayment , PlanPaymentModel >(MemberList.None)
                .ForMember(dest => dest.PlanTitle, opt=>opt.MapFrom(surc =>surc.Plan.Title));
            
            CreateMap<PlanPaymentModel, PlanPayment>(MemberList.None);
            
            CreateMap<PlanPaymentListModel, PlanPayment>(MemberList.None).ReverseMap();
        }
    }
}