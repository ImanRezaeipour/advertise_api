using Advertise.Core.Domain.Specifications;
using Advertise.Core.Model.Specifications;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Specifications
{
    public class SpecificationProfile : BaseProfile
    {
        public SpecificationProfile()
        {
            CreateMap<Specification, SpecificationModel>(MemberList.None).ReverseMap();

            CreateMap<Specification, SpecificationCreateModel>(MemberList.None).ReverseMap();

            CreateMap<Specification, SpecificationEditModel>(MemberList.None).ReverseMap();

            CreateMap<Specification, SpecificationListModel>(MemberList.None).ReverseMap();

            CreateMap<Specification, SpecificationSearchModel>(MemberList.None).ReverseMap();

            CreateMap<Specification, SpecificationDetailModel>(MemberList.None).ReverseMap();
        }
    }
}