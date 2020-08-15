using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Advertise.Core.Common;
using Advertise.Core.Domain.Permissions;
using Advertise.Core.Domain.Roles;
using Advertise.Core.Exceptions;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Permissions;
using Advertise.Core.Objects;
using Advertise.Data.DbContexts;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Advertise.Service.Permissions
{
    public class PermissionService : IPermissionService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<Permission> _permissionRepository;
        private readonly IDbSet<RolePermission> _rolePermissionRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public PermissionService(IUnitOfWork unitOfWork, IMapper mapper, IEventPublisher eventPublisher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _eventPublisher = eventPublisher;
            _permissionRepository = unitOfWork.Set<Permission>();
            _rolePermissionRepository = unitOfWork.Set<RolePermission>();
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(PermissionSearchModel model)
        {
            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(PermissionCreateModel model)
        {
            var permission = _mapper.Map<Permission>(model);
            _permissionRepository.Add(permission);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(permission);
        }

        public async Task DeleteByIdAsync(Guid permissionId)
        {
            var permission = await FindByIdAsync(permissionId);
            _permissionRepository.Remove(permission);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(permission);
        }

        public async Task EditByViewModelAsync(PermissionEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var permission = await FindByIdAsync(model.Id);
            if (permission == null)
                throw new ServiceException();
            _mapper.Map(model, permission);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(permission);
        }

        public async Task<Permission> FindByIdAsync(Guid permissionId)
        {
            return await _permissionRepository.FirstOrDefaultAsync(model => model.Id == permissionId);
        }

        public async Task<IList<PermissionModel>> GetAllPermissionsAsync()
        {
            var request = new PermissionSearchModel
            {
                PageSize = PageSize.All
            };
            var permissions = await GetByRequestAsync(request);
            var permissionsviewModel = _mapper.Map<IList<PermissionModel>>(permissions);

            return permissionsviewModel;
        }

        public async Task<IList<Permission>> GetByRequestAsync(PermissionSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<IList<Permission>> GetPermissionsByRoleIdAsync(Guid roleId)
        {
            var permissionIds = await _rolePermissionRepository.AsNoTracking()
                .Where(model => model.RoleId == roleId)
                .Select(model => model.PermissionId)
                .ToListAsync();

            var permissions = await _permissionRepository.AsNoTracking()
                .Where(model => permissionIds.Contains(model.Id))
                .ToListAsync();

            return permissions;
        }

        public IQueryable<Permission> QueryByRequest(PermissionSearchModel model)
        {
            var permissions = _permissionRepository.AsNoTracking().AsQueryable();

            permissions = permissions.Where(m => m.Name != null);
            if (model.IsCallback != null)
                permissions = permissions.Where(m => m.IsCallback == model.IsCallback);

            if (model.SortMember == null)
                model.SortMember = SortMember.CreatedOn;
            if (model.SortDirection == null)
                model.SortDirection = SortDirection.Asc;

            permissions = permissions.OrderBy($"{model.SortMember} {model.SortDirection}");

            return permissions;
        }

        public async Task<object> GetAllTreeAsync()
        {
            var request = new PermissionSearchModel();
            var permissions = await QueryByRequest(request).ProjectTo<PermissionModel>().ToListAsync();
            return permissions.Select(model => new
            {
                model.Id,
                model.Title,
                model.ParentId,
            });
        }

        public async Task<IList<JsTreeObject>> GetAllTreeByRoleIdAsync(Guid roleId)
        {
            var request = new PermissionSearchModel();
            var permissions = await QueryByRequest(request).ProjectTo<PermissionModel>().ToListAsync();
            var rolePermissions = await GetPermissionsByRoleIdAsync(roleId);
            return permissions.Select(model => new JsTreeObject
            {
                Id = model.Id,
                ParentId = model.ParentId,
                Title = model.Title,
                IsSelect = rolePermissions.Select(m => m.Id).ToList().Contains(model.Id.Value)
            }).ToList();
        }

        public async Task<IList<Guid>> GetIdsByNamesAsync(IList<string> names)
        {
            return await _permissionRepository.AsNoTracking()
                .Where(model => names.Contains(model.Name))
                .Select(model => model.Id)
                .ToListAsync();
        }

        #endregion Public Methods
    }
}