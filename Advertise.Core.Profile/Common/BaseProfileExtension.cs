using AutoMapper;

namespace Advertise.Core.Profile.Common
{
    public static class BaseProfileExtension
    {
        private static IMapper _mapper;

        public static IMapper Mapper
        {
            set { _mapper = value; }
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return _mapper.Map(source, destination);
        }
    }
}
