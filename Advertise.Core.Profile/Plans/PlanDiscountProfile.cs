using Advertise.Core.Domain.Plans;
using Advertise.Core.Model.Plans;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Plans
{
    public class PlanDiscountDiscountProfile : BaseProfile
    {
        public PlanDiscountDiscountProfile()
        {
            CreateMap<PlanDiscountCreateModel, PlanDiscount>(MemberList.None);

            CreateMap<PlanDiscountEditModel, PlanDiscount>(MemberList.None).ReverseMap();

            CreateMap<PlanDiscountListModel, PlanDiscount>(MemberList.None).ReverseMap();

            CreateMap<PlanDiscountModel, PlanDiscount>(MemberList.None).ReverseMap();
        }
    }
}