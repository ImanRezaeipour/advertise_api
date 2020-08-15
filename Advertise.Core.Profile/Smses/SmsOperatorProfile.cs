using Advertise.Core.Domain.Smses;
using Advertise.Core.Model.Smses;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Smses
{
    public class SmsOperatorProfile : BaseProfile
    {
        public SmsOperatorProfile()
        {
            CreateMap<SmsOperator, SmsOperatorModel>(MemberList.None).ReverseMap();

            CreateMap<SmsOperator, SmsOperatorCreateModel>(MemberList.None).ReverseMap();

            CreateMap<SmsOperator, SmsOperatorEditModel>(MemberList.None).ReverseMap();
        }
    }
}