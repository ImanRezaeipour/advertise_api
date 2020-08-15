namespace Advertise.Core.Profile.Common
{
    public class BaseProfile : AutoMapper.Profile
    {
        public override string ProfileName => GetType().Name;
    }
}