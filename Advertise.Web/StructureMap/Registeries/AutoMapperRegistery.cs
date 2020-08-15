using System;
using System.Linq;
using Advertise.Core.Profile.Categories;
using Advertise.Core.Profile.Common;
using AutoMapper;
using StructureMap;

namespace Advertise.Web.StructureMap.Registeries
{
    public class AutoMapperRegistery : Registry
    {
        #region Public Constructors

        public AutoMapperRegistery()
        {
            var profileAssembly = typeof(CategoryProfile).Assembly;
            var profiles = profileAssembly.GetTypes()
                    .Where(type => typeof(Profile).IsAssignableFrom(type) /*&& type != typeof(BaseProfile)*/)
                    .Select(type => (Profile)Activator.CreateInstance(type));

            var config = new MapperConfiguration(expression =>
            {
                foreach (var profile in profiles)
                {
                    expression.AddProfile(profile);
                }
            });

            For<MapperConfiguration>().Use(config);
            For<IMapper>().Use(ctx => ctx.GetInstance<MapperConfiguration>().CreateMapper(ctx.GetInstance));

            BaseProfileExtension.Mapper = config.CreateMapper();

            ConfigStaticProperty(config);
        }

        #endregion Public Constructors

        #region Private Methods

        private static void ConfigStaticProperty(MapperConfiguration config)
        {
        }

        #endregion Private Methods
    }
}