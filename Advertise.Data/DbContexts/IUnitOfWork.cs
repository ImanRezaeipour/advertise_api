using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Advertise.Core.Domain.Common;

namespace Advertise.Data.DbContexts
{
    public interface IUnitOfWork
    {
        bool AutoDetectChangesEnabled { get; set; }
        Database Database { get; }
        bool ForceNoTracking { get; set; }
        bool ProxyCreationEnabled { get; set; }
        bool ValidateOnSaveEnabled { get; set; }
        void AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void Add<Tx>(Tx domain) where Tx : class;
        void BulkInsertData<T>(IEnumerable<T> data);
        void DisableFiltering(string name);
        void EnableFiltering(string name);
        void EnableFiltering(string name, string parameter, object value);
        void ForceDatabaseInitialize();
        IList<T> GetRows<T>(string sql, params object[] parameters) where T : class;
        void MarkAsAdded<TEntity>(TEntity entity) where TEntity : class;
        void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;
        void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class;
        void MarkAsDetached<TEntity>(TEntity entity) where TEntity : class;
        int SaveAllChanges(bool invalidateCacheDependencies = true, Guid? auditUserId = null);
        Task<int> SaveAllChangesAsync(bool invalidateCacheDependencies = true, Guid? auditUserId = null);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        void Remove<TEntity>(TEntity entity) where TEntity : class;
        IDbSet<TEntity> Repository<TEntity>() where TEntity : class;
        void RejectChanges();
        IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new();
        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);
        void Detach(object entity);
        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);
        string CreateDatabaseScript();
    }
}