using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;

namespace Advertise.Data.Migrations
{
    public class ApplicationSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator
    {
        protected override void Generate(DropForeignKeyOperation dropForeignKeyOperation)
        {
            dropForeignKeyOperation.Name = StripDbo(dropForeignKeyOperation.Name);
            base.Generate(dropForeignKeyOperation);
        }
        
        protected override void Generate(DropIndexOperation dropIndexOperation)
        {
            dropIndexOperation.Name = StripDbo(dropIndexOperation.Name);
            base.Generate(dropIndexOperation);
        }

        protected override void Generate(DropPrimaryKeyOperation dropPrimaryKeyOperation)
        {
            dropPrimaryKeyOperation.Name = StripDbo(dropPrimaryKeyOperation.Name);
            base.Generate(dropPrimaryKeyOperation);
        }

        private static string StripDbo(string name)
        {
            return name.Replace("dbo.", "");
        }
    }
}