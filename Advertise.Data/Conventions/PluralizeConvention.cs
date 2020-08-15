using System.Data.Entity.Design.PluralizationServices;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Globalization;

namespace Advertise.Data.Conventions
{
    public class PluralizeConvention : Convention
    {
        public PluralizeConvention()
        {
            var pluralization = PluralizationService.CreateService(new CultureInfo("en-US"));

            // FieldName Convention
            Properties().Configure(property =>
            {
                if (property.ClrPropertyInfo.ReflectedType != null)
                    property.HasColumnName(property.ClrPropertyInfo.ReflectedType.Name + "_" + property.ClrPropertyInfo.Name);
            });

            // TableName Convention
            Types().Configure(entity => entity.ToTable("AD_" + pluralization.Pluralize(entity.ClrType.Name), "dbo"));
        }
    }
}