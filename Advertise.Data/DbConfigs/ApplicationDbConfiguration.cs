using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;
using Advertise.Data.Interceptors;
using ElmahEFLogger.CustomElmahLogger;
using Z.EntityFramework.Plus;

namespace Advertise.Data.DbConfigs
{
    public class ApplicationDbConfiguration : DbConfiguration
    {
        public ApplicationDbConfiguration()
        {
            SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);

            AddInterceptor(new YeKePersianInterceptor());

            //Database.SetInitializer<UnitOfWorkDbContext>(null);

            // config audit when your application is starting up...
            var auditConfiguration = new AuditConfiguration();
            //auditConfiguration.IncludeRelationships = false;
            //auditConfiguration.LoadRelationships = false;
            //auditConfiguration.DefaultAuditable = false;
            //AuditConfiguration.Default.IsAuditable<User>();
            //AuditConfiguration.Default.IsAuditable<Company>();
            //AuditConfiguration.Default.IsAuditable<Product>();

            //ad interception for logg EF errors
            DbInterception.Add(new ElmahEfInterceptor());
        }
    }
}