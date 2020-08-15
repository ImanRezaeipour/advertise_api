using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web;
using Advertise.Core.Common;
using Advertise.Core.Configuration;
using Advertise.Core.Domain.Companies;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.File;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Companies;
using Advertise.Core.Types;
using Advertise.Data.DbContexts;
using AutoMapper;
using FineUploaderObject = Advertise.Core.Objects.FineUploaderObject;

namespace Advertise.Service.Companies
{
    public class CompanyVideoService : ICompanyVideoService
    {
        #region Private Fields

        private readonly IDbSet<Company> _company;
        private readonly IDbSet<CompanyVideo> _companyVideoRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;
        private readonly IConfigurationManager _configurationManager;

        #endregion Private Fields

        #region Public Constructors

        public CompanyVideoService(IMapper mapper, IUnitOfWork unitOfWork, IWebContextManager webContextManager, IEventPublisher eventPublisher, IConfigurationManager configurationManager)
        {
            _unitOfWork = unitOfWork;
            _companyVideoRepository = unitOfWork.Set<CompanyVideo>();
            _company = unitOfWork.Set<Company>();
            _mapper = mapper;
            _webContextManager = webContextManager;
            _eventPublisher = eventPublisher;
            _configurationManager = configurationManager;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountAllVideoByCompanyIdAsync(Guid companyId)
        {
            var request = new CompanyVideoSearchModel
            {
                CompanyId = companyId
            };
            return  await CountByRequestAsync(request);
        }
        
        public async Task<int> CountByRequestAsync(CompanyVideoSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

           return  await QueryByRequest(model).CountAsync();

        }

        public async Task CreateByViewModelAsync(CompanyVideoCreateModel model)
        {
            if (model == null)
                throw new ArgumentException(nameof(model));

            var companyVideo = _mapper.Map<CompanyVideo>(model);
            var companyVideoFiles = _mapper.Map<List<CompanyVideoFile>>(model.CompanyVideoFile);
            companyVideo.State = StateType.Pending;
            companyVideo.CompanyId = (await _company.FirstOrDefaultAsync(m => m.CreatedById == _webContextManager.CurrentUserId)).Id;
            companyVideo.VideoFiles = companyVideoFiles;
            companyVideo.CreatedById = _webContextManager.CurrentUserId;
            companyVideo.CreatedOn = DateTime.Now;
            if (companyVideoFiles != null)
            {
                foreach (var item in companyVideoFiles)
                {
                    item.ThumbName = Path.GetFileNameWithoutExtension(item.FileName) + "_thumb.jpg";
                    if (_configurationManager.VideoWaterMark.ToBoolean())
                        item.WatermarkName = Path.GetFileNameWithoutExtension(item.FileName) + "_watermarked.mp4";
                }
                _companyVideoRepository.Add(companyVideo);

                await _unitOfWork.SaveAllChangesAsync();
            }
            _eventPublisher.EntityInserted(companyVideo);
        }
        
        public async Task DeleteByIdAsync(Guid companyVideoId, bool isCurrentUser = false)
        {
            var companyVideo = await FindByIdAsync(companyVideoId);
            if (isCurrentUser && companyVideo.CreatedById == _webContextManager.CurrentUserId)
                return;

            _companyVideoRepository.Remove(companyVideo);

            await _unitOfWork.SaveAllChangesAsync();
        }

        public async Task EditApproveByViewModelAsync(CompanyVideoEditModel model)
        {
            var companyVideo = await _companyVideoRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            await EditAsync(model, companyVideo);
            companyVideo.ApprovedById = _webContextManager.CurrentUserId;
            companyVideo.State = StateType.Approved;
            _mapper.Map(model, companyVideo);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(companyVideo);
        }

        public async Task EditAsync(CompanyVideoEditModel model, CompanyVideo companyVideo)
        {
            _mapper.Map(model, companyVideo);

            var companyVideoFiles = _mapper.Map<List<CompanyVideoFile>>(model.CompanyVideoFile);
            if (companyVideoFiles != null)
            {
                companyVideo.VideoFiles.Clear();
                companyVideo.VideoFiles.AddRange(companyVideoFiles.ToArray());
            }
            if (companyVideoFiles != null)
                foreach (var item in companyVideoFiles)
                {
                    item.ThumbName = Path.GetFileNameWithoutExtension(item.FileName) + "_thumb.jpg";
                    if (_configurationManager.VideoWaterMark.ToBoolean())
                        item.WatermarkName = Path.GetFileNameWithoutExtension(item.FileName) + "_watermarked.mp4";
                }
        }

        public async Task EditByViewModelAsync(CompanyVideoEditModel model, bool isCurrentUser = false)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var companyVideo = await _companyVideoRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            if(isCurrentUser && companyVideo.CreatedById == _webContextManager.CurrentUserId)
                return;

            await EditAsync(model, companyVideo);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(companyVideo);
        }

        public async Task EditRejectByViewModelAsync(CompanyVideoEditModel model)
        {
            var companyVideo = await _companyVideoRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            await EditAsync(model, companyVideo);

            companyVideo.State = StateType.Rejected;
            _mapper.Map(model, companyVideo);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(companyVideo);
        }

        public async Task<CompanyVideo> FindByIdAsync(Guid? companyVideoId = null ,Guid? userId= null)
        {
            var query = _companyVideoRepository.AsQueryable();
            if (companyVideoId.HasValue)
                query = query.Where(model => model.Id == companyVideoId);
            if (userId.HasValue)
                query = query.Where(model => model.CreatedById == userId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IList<CompanyVideo>> GetApprovedByCompanyIdAsync(Guid companyId)
        {
            var requestVideo = new CompanyVideoSearchModel
            {
                CompanyId = companyId,
                State = StateType.Approved
            };
            var companyVideos = await GetByRequestAsync(requestVideo);

            return companyVideos;
        }

        public async Task<IList<CompanyVideo>> GetByRequestAsync(CompanyVideoSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return  await QueryByRequest(model).ToPagedListAsync();
         }

        public async Task<IList<FineUploaderObject>> GetFilesAsFineUploaderModelByIdAsync(Guid companyVideoId)
        {
            return (await _companyVideoRepository.AsNoTracking()
                    .Include(model => model.VideoFiles)
                    .Where(model => model.Id == companyVideoId)
                    .Select(model => model.VideoFiles)
                    .FirstOrDefaultAsync())
                .Select(model => new FineUploaderObject
                {
                    id = model.Id.ToString(),
                    uuid = model.Id.ToString(),
                    name = model.FileName,
                    thumbnailUrl = FileConst.VideosWebPath.PlusWord(model.FileName),
                    size = new FileInfo(HttpContext.Current.Server.MapPath(FileConst.VideosWebPath.PlusWord(model.FileName))).Length.ToString()
                }).ToList();
        }

        public IQueryable<CompanyVideo> QueryByRequest(CompanyVideoSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            var companyVideos = _companyVideoRepository.AsNoTracking().AsQueryable()
                .Include(m => m.CreatedBy)
                .Include(m => m.CreatedBy.Meta)
                .Include(m => m.Company);
            if (model.Term.HasValue())
                companyVideos = companyVideos.Where(m => m.Title.Contains(model.Term));
            if (model.UserId.HasValue)
                companyVideos = companyVideos.Where(m => m.CreatedById == model.UserId);
            if (model.CompanyId.HasValue)
                companyVideos = companyVideos.Where(m => m.CompanyId == model.CompanyId);
            if (model.Id.HasValue)
                companyVideos = companyVideos.Where(m => m.Id == model.Id);
            if (model.State.HasValue)
                companyVideos = companyVideos.Where(m => m.State == model.State);
            if (model.CreatedById.HasValue)
                companyVideos = companyVideos.Where(m => m.CreatedById == model.CreatedById);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            companyVideos = companyVideos.OrderBy($"{model.SortMember} {model.SortDirection}");

            return companyVideos;
        }

        public async Task RemoveRangeAsync(IList<CompanyVideo> companyVideos)
        {
            if (companyVideos == null)
                throw new ArgumentNullException(nameof(companyVideos));

            foreach (var companyVideo in companyVideos)
                _companyVideoRepository.Remove(companyVideo);
        }

        public async Task SetStateByIdAsync(Guid companyVideoId, StateType state)
        {
            var companyVideo = await FindByIdAsync(companyVideoId);
            companyVideo.State = state;

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(companyVideo);
        }

        #endregion Public Methods
    }
}