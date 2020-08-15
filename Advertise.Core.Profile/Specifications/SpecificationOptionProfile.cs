using Advertise.Core.Domain.Specifications;
using Advertise.Core.Model.Specifications;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Specifications
{
    public class SpecificationOptionsProfile : BaseProfile
    {
        public SpecificationOptionsProfile()
        {
            CreateMap<SpecificationOption, SpecificationOptionCreateModel>(MemberList.None).ReverseMap();

            CreateMap<SpecificationOptionModel, SpecificationOption>(MemberList.None);

            CreateMap<SpecificationOption, SpecificationOptionModel>(MemberList.None)
                .ForMember(dest => dest.CategoryTitle, opts => opts.MapFrom(src => src.Specification.Category.Title))
                .ForMember(dest => dest.SpecificationTitle, opts => opts.MapFrom(src => src.Specification.Title));

            CreateMap<SpecificationOption, SpecificationOptionDetailModel>(MemberList.None).ReverseMap();

            CreateMap<SpecificationOption, SpecificationOptionEditModel>(MemberList.None).ReverseMap();

            CreateMap<SpecificationOption, SpecificationOptionEditModel>(MemberList.None)
                .ForMember(dest => dest.CategoryId, opts => opts.MapFrom(src => src.Specification.CategoryId));

            CreateMap<SpecificationOption, SpecificationOptionListModel>(MemberList.None).ReverseMap();

            CreateMap<SpecificationOption, SpecificationOptionSearchModel>(MemberList.None).ReverseMap();
        }
    }
}