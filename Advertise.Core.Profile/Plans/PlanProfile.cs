using Advertise.Core.Common;
using Advertise.Core.Domain.Plans;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Plans;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Plans
{
    public class PlanProfile : BaseProfile
    {
        public PlanProfile()
        {
            CreateMap<PlanCreateModel, Plan>(MemberList.None);

            CreateMap<PlanEditModel, Plan>(MemberList.None).ReverseMap();

            CreateMap<PlanListModel, Plan>(MemberList.None).ReverseMap();

            CreateMap<PlanModel, Plan>(MemberList.None).ReverseMap();
            
            CreateMap<SelectList, Plan>(MemberList.None).ReverseMap();
        }
    }
}