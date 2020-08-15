using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Advertise.Core.Aspects.Validation;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.File;
using Advertise.Data.Validation.Common;
using Advertise.Service.Categories;
using Advertise.Service.Factory.Users;
using Advertise.Service.Logs;
using Advertise.Service.Products;
using Advertise.Service.Users;
using StructureMap;
using StructureMap.Building.Interception;
using StructureMap.DynamicInterception;
using StructureMap.Pipeline;
using StructureMap.TypeRules;

namespace Advertise.Web.StructureMap.Registeries
{
    public class ServiceLayerRegistery : Registry
    {
        #region Public Constructors

        public ServiceLayerRegistery()
        {
            Scan(scanner =>
            {
                scanner.Assembly("Advertise.Service");
                scanner.WithDefaultConventions();
                scanner.AssemblyContainingType<UserService>();
            });
            
            Scan(scanner =>
            {
                scanner.Assembly("Advertise.Core");
                scanner.WithDefaultConventions();
                scanner.AssemblyContainingType<FileManager>();
            });
            
            Scan(scanner =>
            {
                scanner.Assembly("Advertise.Service.Factory");
                scanner.WithDefaultConventions();
                scanner.AssemblyContainingType<UserFactory>();
            });
            
            Scan(scanner =>
            {
                scanner.Assembly("Advertise.Data.Validation");
                scanner.WithDefaultConventions();
                scanner.AssemblyContainingType<ModelValidator>();
            });

            Policies.SetAllProperties(action => action.OfType<ILogActivityService>());

            Policies.Interceptors(new DynamicProxyInterceptorPolicy((type, instance) => instance.ReturnedType.GetMethods().Any(info => info.HasAttribute<ValidationAttribute>()) && type.Assembly == Assembly.Load("Advertise.Service") && type != typeof(ILogExceptionService) && type != typeof(IUserSettingService) /*&& type != typeof(UserService)*/, typeof(ValidationInterceptor)));
        }

        #endregion Public Constructors
    }

    public class CustomInterception : IInterceptorPolicy
    {
        public string Description => "good interception policy";

        public IEnumerable<IInterceptor> DetermineInterceptors(Type pluginType, Instance instance)
        {
            if (pluginType == typeof(ICategoryService))
            {
                yield return new DecoratorInterceptor(typeof(IProductService), typeof(ProductService));
            }
        }
    }
}