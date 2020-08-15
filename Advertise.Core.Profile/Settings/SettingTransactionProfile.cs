using Advertise.Core.Common;
using Advertise.Core.Domain.Settings;
using Advertise.Core.Model.Common;
using Advertise.Core.Profile.Common;
using AutoMapper;

namespace Advertise.Core.Profile.Settings
{
    public class SettingTransactionProfile :BaseProfile
    {
        public SettingTransactionProfile()
        {
            CreateMap<SettingTransaction, SelectList>(MemberList.None)
                .ForMember(dest => dest.Text, opts => opts.MapFrom(src => src.BankName))
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Id));
        }
    }
}