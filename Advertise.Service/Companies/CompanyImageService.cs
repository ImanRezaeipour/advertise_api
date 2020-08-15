using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web;
using Advertise.Core.Common;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.File;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Core.Objects;
using Advertise.Core.Types;
using Advertise.Data.DbContexts;
using Advertise.Service.Services.Permissions;
using AutoMapper;

namespace Advertise.Service.Companies
{
    public class CompanyImageService : ICompanyImageService
    {
        #region Private Fields

        private readonly IDbSet<CompanyImage> _companyImageRepository;
        private readonly IDbSet<Company> _companyRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CompanyImageService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher)
        {
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _companyRepository = unitOfWork.Set<Company>();
            _companyImageRepository = unitOfWork.Set<CompanyImage>();
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task EditApproveByViewModelAsync(CompanyImageEditModel model)
        {
            var companyImage = await _companyImageRepository.FirstOrDefaultAsync(m => m.Id == model.Id);

            EditAsync(model, companyImage);
            companyImage.ApprovedById = _webContextManager.CurrentUserId;
            companyImage.State = StateType.Approved;
            _mapper.Map(model, companyImage);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityUpdated(companyImage);
        }

        public async Task<int> CountAllByCompanyIdAsync(Guid companyId)
        {
            var request = new CompanyImageSearchModel
            {
                CompanyId = companyId,
            };
            return await CountByRequestAsync(request);
        }

        public async Task<int> CountByRequestAsync(CompanyImageSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(CompanyImageCreateModel model)
        {
            if (model == null)
                throw new ArgumentException(nameof(model));

            var companyImage = _mapper.Map<CompanyImage>(model);
            var companyImageFiles = _mapper.Map<List<CompanyImageFile>>(model.CompanyImageFile);
            companyImage.State = StateType.Pending;
            companyImage.CompanyId = (await _companyRepository.FirstOrDefaultAsync(m => m.CreatedById == _webContextManager.CurrentUserId)).Id;
            companyImage.CreatedById = _webContextManager.CurrentUserId;
            companyImage.CreatedOn = DateTime.Now;
            companyImage.ImageFiles = companyImageFiles;
            _companyImageRepository.Add(companyImage);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(companyImage);
        }

        public async Task DeleteByIdAsync(Guid companyImageId, bool isCurrentUser = false)
        {

            var activityLog = await _companyImageRepository.FirstOrDefaultAsync(model => model.Id == companyImageId);
            if(isCurrentUser && activityLog.CreatedById != _webContextManager.CurrentUserId)
                return;

            _companyImageRepository.Remove(activityLog);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(activityLog);
        }

        public void EditAsync(CompanyImageEditModel model, CompanyImage companyImage)
        {
            _mapper.Map(model, companyImage);
            var companyImageFiles = _mapper.Map<List<CompanyImageFile>>(model.CompanyImageFile);

            companyImage?.ImageFiles.Clear();
            if (companyImageFiles != null)
                foreach (var file in companyImageFiles)
                    if (companyImage != null) companyImage.ImageFiles.Add(file);
        }

        public async Task EditByViewModelAsync(CompanyImageEditModel model, bool isCurrentUser = false)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyImage = await _companyImageRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            if(isCurrentUser && companyImage.CreatedById != _webContextManager.CurrentUserId)
                return;

            EditAsync(model, companyImage);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(companyImage);
        }

        public async Task<CompanyImage> FindByIdAsync(Guid companyImageId, Guid? userId = null)
        {
            var companyImage = _companyImageRepository.AsQueryable();
            companyImage = companyImage.Where(category => category.Id == companyImageId);

            if (userId.HasValue)
                companyImage = companyImage.Where(model => model.CreatedById == userId);

            return await companyImage.FirstOrDefaultAsync();
        }

        public async Task<IList<CompanyImage>> GetApprovedsByCompanyIdAsync(Guid companyId)
        {
            var requestImage = new CompanyImageSearchModel
            {
                CompanyId = companyId,
                State = StateType.Approved
            };
            return await GetByRequestAsync(requestImage);
        }

        public CompanyImage GetById(Guid companyImageId)
        {
            return _companyImageRepository
                .AsNoTracking()
                .FirstOrDefault(model => model.Id == companyImageId);
        }

        public async Task<IList<CompanyImage>> GetByRequestAsync(CompanyImageSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<IList<FineUploaderObject>> GetFilesAsFineUploaderModelByIdAsync(Guid companyImageId)
        {
            return (await _companyImageRepository.AsNoTracking()
                    .Include(model => model.ImageFiles)
                    .Where(model => model.Id == companyImageId)
                    .Select(model => model.ImageFiles)
                    .SingleOrDefaultAsync())
                .Select(model => new FineUploaderObject
                {
                    id = model.Id.ToString(),
                    uuid = model.Id.ToString(),
                    name = model.FileName,
                    thumbnailUrl = FileConst.ImagesWebPath.PlusWord(model.FileName),
                    size = new FileInfo(HttpContext.Current.Server.MapPath(FileConst.ImagesWebPath.PlusWord(model.FileName))).Length.ToString()
                }).ToList();
        }

        public async Task<bool> IsMineAsync(Guid companyImageId)
        {
            var company = await _companyImageRepository.FirstOrDefaultAsync(model => model.Id == companyImageId);
            return (company.CreatedById == _webContextManager.CurrentUserId || HttpContext.Current.User.IsInRole(PermissionConst.CanCompanyImageEdit));
        }

        public IQueryable<CompanyImage> QueryByRequest(CompanyImageSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyImages = _companyImageRepository.AsNoTracking().AsQueryable()
                .Include(m => m.CreatedBy)
                .Include(m => m.CreatedBy.Meta)
                .Include(m => m.Company)
                .Include(m => m.ImageFiles);
            if (model.Term.HasValue())
                companyImages = companyImages.Where(m => m.Title.Contains(model.Term));
            if (model.UserId.HasValue)
                companyImages = companyImages.Where(m => m.CreatedById == model.UserId);
            if (model.CompanyId.HasValue)
                companyImages = companyImages.Where(m => m.CompanyId == model.CompanyId);
            if (model.CreatedById.HasValue)
                companyImages = companyImages.Where(m => m.CreatedById == model.CreatedById);
            if (model.State.HasValue)
                companyImages = companyImages.Where(m => m.State == model.State);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            companyImages = companyImages.OrderBy($"{model.SortMember} {model.SortDirection}");

            return companyImages;
        }

        public async Task EditRejectByViewModelAsync(CompanyImageEditModel model)
        {
            var companyImage = await _companyImageRepository.FirstOrDefaultAsync(m => m.Id == model.Id);

            EditAsync(model, companyImage);
            _mapper.Map(model, companyImage);
            companyImage.State = StateType.Rejected;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.Publish(companyImage);
        }

        public async Task RemoveRangeAsync(IList<CompanyImage> companyImages)
        {
            if (companyImages == null)
                throw new ArgumentNullException(nameof(companyImages));

            foreach (var companyImage in companyImages)
                _companyImageRepository.Remove(companyImage);
        }

        public async Task SetStateByIdAsync(Guid id, StateType state)
        {
            var companyImage = await FindByIdAsync(id);
            companyImage.State = state;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(companyImage);
        }

        #endregion Public Methods
    }
}