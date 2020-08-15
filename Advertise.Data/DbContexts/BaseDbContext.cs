using Advertise.Core.Exceptions;
using Advertise.Core.Types;
using Advertise.Data.Constants;
using Advertise.Data.Conventions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Advertise.Core.Domain.Common;
using Advertise.Core.Domain.Roles;
using Advertise.Core.Domain.Users;
using Advertise.Core.Extensions;
using Advertise.Core.Helpers;
using Advertise.Core.Mapping.Common;

namespace Advertise.Data.DbContexts
{
    public class BaseDbContext : IdentityDbContext<User, Role, Guid, UserLogin, UserRole, UserClaim>, IUnitOfWork
    {
        #region Public Constructors

        public BaseDbContext() : base(UowConst.ConnectionString)
        {
            //((IObjectContextAdapter) this).ObjectContext.ContextOptions.LazyLoadingEnabled = true;
        }

        #endregion Public Constructors

        #region Public Properties

        public virtual bool AutoDetectChangesEnabled
        {
            get
            {
                return this.Configuration.AutoDetectChangesEnabled;
            }
            set
            {
                this.Configuration.AutoDetectChangesEnabled = value;
            }
        }

        public bool ForceNoTracking { get; set; }

        public virtual bool ProxyCreationEnabled
        {
            get
            {
                return this.Configuration.ProxyCreationEnabled;
            }
            set
            {
                this.Configuration.ProxyCreationEnabled = value;
            }
        }

        public bool ValidateOnSaveEnabled
        {
            get
            {
                return Configuration.ValidateOnSaveEnabled;
            }
            set
            {
                Configuration.ValidateOnSaveEnabled = value;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().Add(entity);
            SaveAllChanges();
        }

        public void AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            ((DbSet<TEntity>)Set<TEntity>()).AddRange(entities);
        }

        public void BulkInsertData<T>(IEnumerable<T> data)
        {
            AutoDetectChangesEnabled = false;
            ValidateOnSaveEnabled = false;
            // base.BulkInsertData<T>(data);
        }

        public string CreateDatabaseScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        public void Detach(object entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            ((IObjectContextAdapter)this).ObjectContext.Detach(entity);
        }

        public void DisableFiltering(string name)
        {
            //this.DisableFilter(name);
        }

        public void EnableFiltering(string name)
        {
            //this.EnableFilter(name);
        }

        public void EnableFiltering(string name, string parameter, object value)
        {
            //this.EnableFilter(name).SetParameter(parameter, value);
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            var transactionalBehavior = doNotEnsureTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;
            var result = this.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            return result;
        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            //add parameters to command
            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i <= parameters.Length - 1; i++)
                {
                    var p = parameters[i] as DbParameter;
                    if (p == null)
                        throw new Exception("Not support parameter type");

                    commandText += i == 0 ? " " : ", ";

                    commandText += "@" + p.ParameterName;
                    if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                    {
                        //output parameter
                        commandText += " output";
                    }
                }
            }

            var result = this.Database.SqlQuery<TEntity>(commandText, parameters).ToList();

            //performance hack applied as described here - http://www.nopcommerce.com/boards/t/25483/fix-very-important-speed-improvement.aspx
            bool acd = this.Configuration.AutoDetectChangesEnabled;
            try
            {
                this.Configuration.AutoDetectChangesEnabled = false;

                for (int i = 0; i < result.Count; i++)
                    result[i] = AttachEntityToContext(result[i]);
            }
            finally
            {
                this.Configuration.AutoDetectChangesEnabled = acd;
            }

            return result;
        }

        public void ForceDatabaseInitialize()
        {
            Database.Initialize(true);
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public void MarkAsAdded<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Added;
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public void MarkAsDetached<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Detached;
        }

        public void RejectChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;

                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;

                    case EntityState.Detached:
                        break;

                    case EntityState.Unchanged:
                        break;

                    case EntityState.Deleted:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().Remove(entity);
            SaveAllChanges();
        }

        public IDbSet<TEntity> Repository<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public int SaveAllChanges(bool invalidateCacheDependencies = true, Guid? auditUserId = null)
        {
            UpdateValue(auditUserId);

            var result = SaveChanges();
            //if (result <= 0)
            //    throw new ServiceException("");

            return GetSaveChangeResult(invalidateCacheDependencies, result);
        }

        public async Task<int> SaveAllChangesAsync(bool invalidateCacheDependencies = true, Guid? auditUserId = null)
        {
            UpdateValue(auditUserId);

            var result = await SaveChangesAsync();
            //if (result <= 0)
            //    throw new ServiceException("");

            return GetSaveChangeResult(invalidateCacheDependencies, result);
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return this.Database.SqlQuery<TElement>(sql, parameters);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
        {
            //little hack here until Entity Framework really supports stored procedures
            //otherwise, navigation properties of loaded entities are not loaded until an entity is attached to the context
            var alreadyAttached = Set<TEntity>().Local.FirstOrDefault(x => x.Id == entity.Id);
            if (alreadyAttached == null)
            {
                //attach new entity
                Set<TEntity>().Attach(entity);
                return entity;
            }

            //entity is already loaded
            return alreadyAttached;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.Conventions.Add(new PluralizeConvention());

            modelBuilder.Configurations.AddFromAssembly(typeof(BaseConfig<>).Assembly);

            AddEntities(typeof(BaseEntity).Assembly, modelBuilder, UowConst.EntitiesNamespace);
        }

        #endregion Protected Methods

        #region Private Methods

        private static void AddEntities(Assembly assembly, DbModelBuilder modelBuilder, string nameSpace)
        {
            var entityTypes = assembly.GetTypes()
                .Where(type => type.BaseType != null && type.Namespace == nameSpace && type.BaseType == null)
                .ToList();
            entityTypes.ForEach(modelBuilder.RegisterEntityType);
        }

        private static int GetSaveChangeResult(bool invalidateCacheDependencies, int result)
        {
            if (!invalidateCacheDependencies)
                return result;
            //var changedEntityNames = GetChangedEntityNames();
            //new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
            return result;
        }

        private static void UpdateFieldsForAdded(DbEntityEntry<BaseEntity> entry)
        {
            //var auditUserIp = HttpContext.Current.Request.GetIp();
            //var auditUserAgent = HttpContext.Current.Request.GetBrowser();
            var auditDate = DateTime.Now;
            if (entry.Entity.Id == Guid.Empty)
                entry.Entity.Id = SequentialGuidGenerator.NewSequentialGuid();
           // entry.Entity.CreatedOn = auditDate;
           // entry.Entity.ModifiedOn = auditDate;
            //entry.Entity.Audit = AuditType.Create;
            //entry.Entity.CreatorIp = auditUserIp;
            //entry.Entity.ModifierIp = auditUserIp;
            //entry.Entity.CreatorAgent = auditUserAgent;
            //entry.Entity.ModifierAgent = auditUserAgent;
            //entry.Entity.Version = 1;
        }

        private static void UpdateFieldsForModified(DbEntityEntry<BaseEntity> entry)
        {
            //var auditUserIp = HttpContext.Current.Request.GetIp();
            //var auditUserAgent = HttpContext.Current.Request.GetBrowser();
            var auditDate = DateTime.Now;
           // entry.Entity.ModifiedOn = auditDate;
            //entry.Entity.ModifierIp = auditUserIp;
            //entry.Entity.ModifierAgent = auditUserAgent;
            //entry.Entity.Version = ++entry.Entity.Version;
            //entry.Entity.Audit = entry.Entity.IsDelete.GetValueOrDefault()
                //? AuditType.SoftDelete
                //: AuditType.Edit;
            //entry.Entity.Version = 1;
        }

        private string[] GetChangedEntityNames()
        {
            return ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added ||
                            x.State == EntityState.Modified ||
                            x.State == EntityState.Deleted)
                .Select(x => ObjectContext.GetObjectType(x.Entity.GetType()).FullName)
                .Distinct()
                .ToArray();
        }

        private void UpdateAuditFields(Guid auditUserId)
        {
            var auditUserIp = HttpContext.Current.Request.GetIp();
            var auditUserAgent = HttpContext.Current.Request.GetBrowser();
            var auditDate = DateTime.Now;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                // Note: You must add a reference to assembly : System.Data.Entity
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity.Id == Guid.Empty)
                            entry.Entity.Id = SequentialGuidGenerator.NewSequentialGuid();
                        //entry.Entity.CreatedById = auditUserId;
                        //entry.Entity.ModifiedById = auditUserId;
                        UpdateFieldsForAdded(entry);
                        break;

                    case EntityState.Modified:
                        //entry.Entity.ModifiedById = auditUserId;
                        UpdateFieldsForModified(entry);
                        break;

                    case EntityState.Detached:
                        break;

                    case EntityState.Unchanged:
                        break;

                    case EntityState.Deleted:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            foreach (var entry in ChangeTracker.Entries<Role>())
            {
                // Note: You must add a reference to assembly : System.Data.Entity
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity.Id == Guid.Empty)
                            entry.Entity.Id = SequentialGuidGenerator.NewSequentialGuid();
                        entry.Entity.CreatedOn = auditDate;
                        entry.Entity.CreatedById = auditUserId;
                        entry.Entity.ModifiedOn = auditDate;
                        entry.Entity.Audit = AuditType.Create;
                       // entry.Entity.ModifiedById = auditUserId;
                        entry.Entity.CreatorIp = auditUserIp;
                        entry.Entity.ModifierIp = auditUserIp;
                        entry.Entity.CreatorAgent = auditUserAgent;
                        entry.Entity.ModifierAgent = auditUserAgent;
                        entry.Entity.Version = 1;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedOn = auditDate;
                       // entry.Entity.ModifiedById = auditUserId;
                        entry.Entity.ModifierIp = auditUserIp;
                        entry.Entity.ModifierAgent = auditUserAgent;
                        entry.Entity.Version = ++entry.Entity.Version;
                        entry.Entity.Audit = entry.Entity.IsDelete.GetValueOrDefault()
                            ? AuditType.SoftDelete
                            : AuditType.Edit;
                        break;

                    case EntityState.Detached:
                        break;

                    case EntityState.Unchanged:
                        break;

                    case EntityState.Deleted:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void UpdateEntityFields()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                // Note: You must add a reference to assembly : System.Data.Entity
                switch (entry.State)
                {
                    case EntityState.Added:
                        UpdateFieldsForAdded(entry);

                        break;

                    case EntityState.Modified:
                        UpdateFieldsForModified(entry);
                        break;

                    case EntityState.Detached:
                        break;

                    case EntityState.Unchanged:
                        break;

                    case EntityState.Deleted:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void UpdateValue(Guid? auditUserId)
        {
            if (auditUserId == null || auditUserId == Guid.Empty)
            {
                if (HttpContext.Current == null)
                {
                     UpdateEntityFields();
                    return;
                }
                if (HttpContext.Current.User == null)
                {
                    UpdateEntityFields();
                    return;
                }
                if (HttpContext.Current.User.Identity == null)
                {
                    UpdateEntityFields();
                    return;
                }
                var currentUserId = HttpContext.Current.User.Identity.IsAuthenticated ? Guid.Parse(HttpContext.Current.User.Identity.GetUserId()) : Guid.Empty;
                if (currentUserId == Guid.Empty)
                    UpdateEntityFields();
                else
                    UpdateAuditFields(currentUserId);
            }
            else
                UpdateAuditFields(auditUserId.Value);
        }

        #endregion Private Methods
    }
}