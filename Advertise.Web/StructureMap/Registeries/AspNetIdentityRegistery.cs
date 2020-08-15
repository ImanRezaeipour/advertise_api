using System;
using System.Data.Entity;
using Advertise.Core.Domain.Roles;
using Advertise.Core.Domain.Users;
using Advertise.Data.DbContexts;
using Advertise.Service.Messages;
using Advertise.Service.Roles;
using Advertise.Service.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using StructureMap;
using StructureMap.Web;

namespace Advertise.Web.StructureMap.Registeries
{
    public class AspNetIdentityRegistery : Registry
    {
        #region Public Constructors

        public AspNetIdentityRegistery()
        {
            For<BaseDbContext>().HybridHttpOrThreadLocalScoped().Use(context => (BaseDbContext)context.GetInstance<IUnitOfWork>());
            For<DbContext>().HybridHttpOrThreadLocalScoped().Use(context => (BaseDbContext)context.GetInstance<IUnitOfWork>());
            For<IUserStore<User, Guid>>().HybridHttpOrThreadLocalScoped().Use<UserStore<User, Role, Guid, UserLogin, UserRole, UserClaim>>();
            For<IRoleStore<Role, Guid>>().HybridHttpOrThreadLocalScoped().Use<RoleStore<Role, Guid, UserRole>>();
            For<IAuthenticationManager>().Use<UserAuthenticationManager>();
            For<IUserSignInService>().HybridHttpOrThreadLocalScoped().Use<UserSignInService>();
            For<IRoleService>().HybridHttpOrThreadLocalScoped().Use<RoleService>();
            For<IIdentityMessageService>().Use<SmsService>();
            For<IIdentityMessageService>().Use<EmailService>();
            For<IUserService>().HybridHttpOrThreadLocalScoped().Use<UserService>()
                .Ctor<IIdentityMessageService>("smsService").Is<SmsService>()
                .Ctor<IIdentityMessageService>("emailService").Is<EmailService>()
                .Setter(userService => userService.SmsService).Is<SmsService>()
                .Setter(userService => userService.EmailService).Is<EmailService>();
            //For<UserService>().HybridHttpOrThreadLocalScoped().Use(context => (UserService) context.GetInstance<IUserService>())
            //    .AddInterceptor(new DecoratorInterceptor(typeof(ValidationInterceptor),typeof(ValidationInterceptor)));
            //For<IRoleStore>().HybridHttpOrThreadLocalScoped().Use<RoleStore>();
            //For<IUserStore>().HybridHttpOrThreadLocalScoped().Use<UserStore>();
        }

        #endregion Public Constructors
    }
}