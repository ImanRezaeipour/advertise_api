using Advertise.Core.Domain.Guarantees;
using Advertise.Core.Model.Guarantees;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Guarantees
{
    public class GuaranteeProfile : BaseProfile
    {
        public GuaranteeProfile()
        {
            CreateMap<Guarantee, GuaranteeCreateModel>(MemberList.None).ReverseMap();

            CreateMap<Guarantee, GuaranteeEditModel>(MemberList.None).ReverseMap();

            CreateMap<GuaranteeModel, Guarantee>(MemberList.None).ReverseMap();

            CreateMap<Guarantee, GuaranteeListModel>(MemberList.None).ReverseMap();
        }
    }
}