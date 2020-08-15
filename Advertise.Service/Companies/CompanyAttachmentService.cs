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
using AutoMapper;

namespace Advertise.Service.Companies
{
    public class CompanyAttachmentService : ICompanyAttachmentService
    {
        #region Private Fields

        private readonly IDbSet<CompanyAttachment> _companyAttachmentRepository;
        private readonly IDbSet<Company> _companyRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;

        #endregion Private Fields

        #region Public Constructors

        public CompanyAttachmentService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher)
        {
            _unitOfWork = unitOfWork;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _companyRepository = unitOfWork.Set<Company>();
            _companyAttachmentRepository = unitOfWork.Set<CompanyAttachment>();
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task EditApproveByViewModelAsync(CompanyAttachmentEditModel model)
        {
            var companyAttachment =
                await _companyAttachmentRepository.FirstOrDefaultAsync(m => m.Id == model.Id);

            await EditAsync(model, companyAttachment);
            _mapper.Map(model, companyAttachment);
            companyAttachment.ApprovedById = _webContextManager.CurrentUserId;
            companyAttachment.State = StateType.Approved;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(companyAttachment);
        }

        public async Task<int> CountByRequestAsync(CompanyAttachmentSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(CompanyAttachmentCreateModel model)
        {
            if (model == null)
                throw new ArgumentException(nameof(model));

            var companyAttachment = _mapper.Map<CompanyAttachment>(model);
            var companyAttachmentFiles = _mapper.Map<List<CompanyAttachmentFile>>(model.CompanyAttachmentFile);
            companyAttachmentFiles.ForEach(file =>
            {
                file.FileSize = new FileInfo( HttpContext.Current.Server.MapPath(FileConst.ImagesWebPath.PlusWord(file.FileName))).Length.ToString();
                file.FileExtension = new FileInfo(HttpContext.Current.Server.MapPath(FileConst.ImagesWebPath.PlusWord(file.FileName))).Extension.ToString().Replace(".","");
            });
            companyAttachment.State = StateType.Pending;
            companyAttachment.CompanyId = (await _companyRepository.FirstOrDefaultAsync(m => m.CreatedById == _webContextManager.CurrentUserId)).Id;
            companyAttachment.CreatedById = _webContextManager.CurrentUserId;
            companyAttachment.AttachmentFiles = companyAttachmentFiles;
            companyAttachment.CreatedOn = DateTime.Now;
            _companyAttachmentRepository.Add(companyAttachment);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(companyAttachment);
        }

        public async Task DeleteByIdAsync(Guid companyAttachmentId, bool isCurrentUser = false)
        {
            var companyAttachment = await _companyAttachmentRepository.FirstOrDefaultAsync(model => model.Id == companyAttachmentId);
            if (isCurrentUser && companyAttachment.CreatedById != _webContextManager.CurrentUserId)
                return;

            _companyAttachmentRepository.Remove(companyAttachment);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(companyAttachment);
        }

        public async Task EditAsync(CompanyAttachmentEditModel model, CompanyAttachment companyAttachment)
        {
            var companyAttachmentMap = _mapper.Map(model, companyAttachment);
            var companyAttachmentFiles = _mapper.Map<List<CompanyAttachmentFile>>(model.CompanyAttachmentFile);
            if (companyAttachment != null)
            {
                companyAttachment.AttachmentFiles.Clear();
                if (companyAttachmentFiles != null)
                    foreach (var file in companyAttachmentFiles)
                        companyAttachment.AttachmentFiles.Add(file);
            }
        }

        public async Task EditByViewModelAsync(CompanyAttachmentEditModel model, bool isCurrentUser = false)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyAttachment = await FindByIdAsync(model.Id);
            if(isCurrentUser && companyAttachment.CreatedById != _webContextManager.CurrentUserId)
                return;

            await EditAsync(model, companyAttachment);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(companyAttachment);
        }

        public async Task<CompanyAttachment> FindByIdAsync(Guid companyAttachmentId, Guid? userId = null)
        {
            var query = _companyAttachmentRepository.AsQueryable();
            query = query.Where(category => category.Id == companyAttachmentId);

            if (userId.HasValue)
                query = query.Where(category => category.CreatedById == userId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IList<CompanyAttachment>> GetApprovedByCompanyIdAsync(Guid companyId)
        {
            var request = new CompanyAttachmentSearchModel
            {
                CompanyId = companyId,
                State = StateType.Approved
            };

            return await GetByRequestAsync(request);
        }

        public CompanyAttachment GetById(Guid companyAttachmentId)
        {
            return _companyAttachmentRepository
                .AsNoTracking()
                .FirstOrDefault(model => model.Id == companyAttachmentId);
        }

        public async Task<IList<CompanyAttachment>> GetByRequestAsync(CompanyAttachmentSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<IList<FineUploaderObject>> GetFilesAsFineUploaderModelByIdAsync(Guid companyAttachmentId)
        {
            return (await _companyAttachmentRepository.AsNoTracking()
                    .Include(model => model.AttachmentFiles)
                    .Where(model => model.Id == companyAttachmentId)
                    .Select(model => model.AttachmentFiles)
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

        public async Task<bool> IsMineAsync(Guid companyAttachmentId)
        {
            var product = await _companyAttachmentRepository
                .AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == companyAttachmentId);

            return product.CreatedById == _webContextManager.CurrentUserId;
        }

        public IQueryable<CompanyAttachment> QueryByRequest(CompanyAttachmentSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyAttachments = _companyAttachmentRepository.AsNoTracking().AsQueryable()
                .Include(m => m.CreatedBy)
                .Include(m => m.CreatedBy.Meta)
                .Include(m => m.Company)
                .Include(m => m.AttachmentFiles);

            if (model.Term.HasValue())
                companyAttachments = companyAttachments.Where(m => m.Title.Contains(model.Term));
            if (model.UserId.HasValue)
                companyAttachments = companyAttachments.Where(m => m.CreatedById == model.UserId);
            if (model.Id.HasValue)
                companyAttachments = companyAttachments.Where(m => m.Id == model.Id);
            if (model.CompanyId.HasValue)
                companyAttachments = companyAttachments.Where(m => m.CompanyId == model.CompanyId);
            if (model.CreatedById.HasValue)
                companyAttachments = companyAttachments.Where(m => m.CreatedById == model.CreatedById);

            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;

            companyAttachments = companyAttachments.OrderBy($"{model.SortMember} {model.SortDirection}");

            return companyAttachments;
        }

       public async Task EditRejectByViewModelAsync(CompanyAttachmentEditModel model)
        {
            var companyAttachment = await _companyAttachmentRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            await EditAsync(model, companyAttachment);

            _mapper.Map(model, companyAttachment);
            companyAttachment.State = StateType.Rejected;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(companyAttachment);
        }

        public async Task RemoveRangeAsync(IList<CompanyAttachment> companyAttachments)
        {
            if (companyAttachments == null)
                throw new ArgumentNullException(nameof(companyAttachments));

            foreach (var companyAttachment in companyAttachments)
                _companyAttachmentRepository.Remove(companyAttachment);
        }

        public async Task SetStateByIdAsync(Guid id, StateType state)
        {
            var companyAttachment = await FindByIdAsync(id);
            companyAttachment.State = state;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(companyAttachment);
        }

        #endregion Public Methods
    }
}