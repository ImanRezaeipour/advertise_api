using System.Data.Entity.ModelConfiguration;

namespace Advertise.Core.Mapping.Common
{
    public abstract class BaseConfig<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : class 
    {
        protected BaseConfig()
        {
            PostInitialize();
        }

        protected virtual void PostInitialize()
        {

        }
    }
}