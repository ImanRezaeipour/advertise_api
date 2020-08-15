using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Roles;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Roles;
using Advertise.Data.DbContexts;
using Advertise.Service.Common;
using Advertise.Service.Permissions;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace Advertise.Service.Roles
{
    public class RoleService : RoleManager<Role, Guid>, IRoleService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IWebContextManager _webContextManager;
        private readonly IPermissionService _permissionService;
        private readonly IDbSet<RolePermission> _rolePermissionRepository;
        private readonly IDbSet<Role> _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper, IRoleStore<Role, Guid> roleStore, IEventPublisher eventPublisher, IPermissionService permissionService, IPermissionService permissionService1, IWebContextManager webContextManager)
            : base(roleStore)
        {
            _roleRepository = unitOfWork.Set<Role>();
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _permissionService = permissionService;
            _permissionService = permissionService1;
            _webContextManager = webContextManager;
            _rolePermissionRepository = unitOfWork.Set<RolePermission>();
            _mapper = mapper;
            AutoCommitEnabled = true;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool AutoCommitEnabled { get; set; }
        
        #endregion Public Properties

        #region Public Methods

        public async Task<int> CountByRequestAsync(RoleSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var roles = await QueryByRequest(model).CountAsync();

            return roles;
        }

        public async Task CreateByViewModelAsync(RoleCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var role = _mapper.Map<Role>(model);
            role.Code = await GenerateCodeAsync();

            var permissionIds = model.Permissions.Split(',');
            role.RolePermissions = new HashSet<RolePermission>();
            permissionIds.ForEach(permissionId => role.RolePermissions.Add(new RolePermission {PermissionId = permissionId.ToGuidOrDefault()}));
            role.CreatedById = _webContextManager.CurrentUserId;
            role.CreatedOn = DateTime.Now;
            _roleRepository.Add(role);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(role);
        }

        public async Task DeleteByIdAsync(Guid roleId)
        {
            if (roleId == null)
                throw new ArgumentNullException(nameof(roleId));

            var role = await FindByIdAsync(roleId);
            _roleRepository.Remove(role);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(role);
        }

        public async Task EditByViewModelAsync(RoleEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var role = await _roleRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, role);

            var permissionIds = model.Permissions.Split(',');
            role.RolePermissions = new HashSet<RolePermission>();
            permissionIds.ForEach(permissionId => role.RolePermissions.Add(new RolePermission { PermissionId = permissionId.ToGuidOrDefault() }));

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(role);
        }

        public async Task<Role> FindAsync(Guid roleId)
        {
            return await _roleRepository.FirstOrDefaultAsync(model => model.Id == roleId);
        }

        public async Task<Role> FindByUserIdAsync(Guid userId)
        {
            var query = from role in Roles
                        from user in role.Users
                        where user.UserId == userId
                        select role;

            return await query.FirstOrDefaultAsync();
        }

        public async Task<string> GenerateCodeAsync()
        {
            var request = new RoleSearchModel();
            var maxCode = await MaxByRequestAsync(request, RoleAggregateMemberModel.Code);
            return maxCode == null ? CodeConst.BeginNumber5Digit : maxCode.ExtractNumeric();
        }

        public async Task<IList<string>> GetPermissionNamesByUserIdAsync(Guid userId)
        {
            var permissionNames = new List<string>();

            var userRoles = await GetRolesByUserIdAsync(userId);
            foreach (var role in userRoles)
            {
                permissionNames.Add(role.Name);
                var rolePermissions = await _permissionService.GetPermissionsByRoleIdAsync(role.Id);
                rolePermissions.ForEach(permission =>
                {
                    permissionNames.Add(permission.Name);
                });
            }

            return permissionNames;
        }

        public async Task<IList<string>> GetRoleNamesByUserIdAsync(Guid userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            var result = await userRolesQuery
                .OrderBy(model => model.Name)
                .Select(model => model.Name)
                .ToListAsync();

            return result;
        }

        public async Task<IList<SelectList>> GetRolesAsSelectListAsync()
        {
            var roles = await _roleRepository
                .AsNoTracking()
                .Select(record => new SelectList
                {
                    Value = record.Id.ToString(),
                    Text = record.Name
                })
                .ToListAsync();

            return roles;
        }

        public async Task<IList<Role>> GetRolesByPermissionIdAsync(Guid permissionId)
        {
            var roleIds = await _rolePermissionRepository.AsNoTracking()
                .Where(model => model.PermissionId == permissionId)
                .Select(model => model.RoleId)
                .ToListAsync();

            var roles = await _roleRepository.AsNoTracking()
                .Where(model => roleIds.Contains(model.Id))
                .ToListAsync();

            return roles;
        }

        public async Task<IList<Role>> GetRolesByRequestAsync(RoleSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<IList<Role>> GetRolesByUserIdAsync(Guid userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            var roles = await userRolesQuery.AsNoTracking()
                .ToListAsync();

            return roles;
        }

        public async Task<bool> IsExistNameAsync(string roleName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _roleRepository.AsNoTracking().AnyAsync(model => model.Name == roleName);
        }

        public async Task<bool> IsSystemRoleAsync(Guid roleId)
        {
            if (roleId == null)
                throw new ArgumentNullException(nameof(roleId));

            return await _roleRepository.AsNoTracking()
                .AnyAsync(model => model.Id == roleId && model.IsSystemRole == true);
        }

        public async Task<string> MaxByRequestAsync(RoleSearchModel model, string aggregateMember)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var products = QueryByRequest(model);
            switch (aggregateMember)
            {
                case "Code":
                    var memberMax = await products.MaxAsync(m => m.Code);
                    return memberMax;
            }

            return null;
        }

        public IQueryable<Role> QueryByRequest(RoleSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var roles = _roleRepository.AsNoTracking().AsQueryable();

            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;

            roles = roles.OrderBy($"{model.SortMember} {model.SortDirection}");

            return roles;
        }

        #endregion Public Methods
    }
}