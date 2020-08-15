using Advertise.Core.Domain.Companies;
using Advertise.Core.Model.Companies;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Companies
{
    public class CompanyQuestionLikeProfile : BaseProfile
    {
        public CompanyQuestionLikeProfile()
        {
            CreateMap<CompanyQuestionLike, CompanyQuestionLikeCreateModel>(MemberList.None).ReverseMap();
        }
    }
}