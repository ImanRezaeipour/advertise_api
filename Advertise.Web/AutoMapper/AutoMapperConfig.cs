using System;
using System.Linq;
using Advertise.Core.Profile.Categories;
using AutoMapper;

namespace Advertise.Web.AutoMapper
{
    public class AutoMapperConfig
    {
        #region Public Methods

        public static void RegisterAutoMapper()
        {
            var profileAssembly = typeof(CategoryProfile).Assembly;
            var profiles = profileAssembly.GetTypes()
                .Where(t => typeof(Profile).IsAssignableFrom(t))
                .Select(t => (Profile)Activator.CreateInstance(t));

            Mapper.Initialize(expression =>
            {
                foreach (var profile in profiles)
                {
                    expression.AddProfile(profile);
                }
            });
        }

        #endregion Public Methods
    }
}