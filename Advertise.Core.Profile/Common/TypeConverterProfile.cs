using System;

namespace Advertise.Core.Profile.Common
{
    public class TypeConverterProfile : BaseProfile
    {
        public TypeConverterProfile()
        {
            //CreateMap<DateTime, string>().ConvertUsing(new DateTimeTypeConverter());
            //CreateMap<DateTime?, DateTime?>().ConvertUsing(new NullableDateTimeTypeConverter());
            CreateMap<DateTime?, DateTime?>().ProjectUsing(source => source ?? new DateTime(1989, 1, 15));
        }
    }
}