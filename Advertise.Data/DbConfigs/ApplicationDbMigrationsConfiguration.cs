using System.Data.Entity.Migrations;
using Advertise.Data.Constants;
using Advertise.Data.DbContexts;
using Advertise.Data.Migrations;

namespace Advertise.Data.Migrations
{
    public sealed class ApplicationDbMigrationsConfiguration : DbMigrationsConfiguration<BaseDbContext>
    {
        public ApplicationDbMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            SetSqlGenerator(UowConst.SqlClientNamespace, new ApplicationSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(BaseDbContext context)
        {
        }
    }
}