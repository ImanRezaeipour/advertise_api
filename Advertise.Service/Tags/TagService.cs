using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web;
using Advertise.Core.Common;
using Advertise.Core.Domain.Tags;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.File;
using Advertise.Core.Model.Common;
using Advertise.Core.Model.Tags;
using Advertise.Data.DbContexts;
using Advertise.Service.Common;
using AutoMapper;
using FineUploaderObject = Advertise.Core.Objects.FineUploaderObject;

namespace Advertise.Service.Tags
{
    public class TagService : ITagService
    {
        #region Private Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IDbSet<Tag> _tagRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion Private Fields

        #region Public Constructors

        public TagService(IMapper mapper, IUnitOfWork unitOfWork, IEventPublisher eventPublisher)
        {
            _mapper = mapper;
            _tagRepository = unitOfWork.Set<Tag>();
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(TagSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).CountAsync();
        }

        public async Task CreateByViewModelAsync(TagCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var tag = _mapper.Map<Tag>(model);
            tag.Code = await GenerateCodeForTagAsync();
            tag.CreatedOn = DateTime.Now;
            _tagRepository.Add(tag);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityInserted(tag);
        }

        public async Task DeleteByIdAsync(Guid tagId)
        {
            if (tagId == null)
                throw new ArgumentNullException(nameof(tagId));

            var tag = await _tagRepository.FirstOrDefaultAsync(model => model.Id == tagId);
            _tagRepository.Remove(tag);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityDeleted(tag);
        }

        public async Task EditByViewModelAsync(TagEditModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var tag = await _tagRepository.FirstOrDefaultAsync(m => m.Id == model.Id);
            _mapper.Map(model, tag);

            await _unitOfWork.SaveAllChangesAsync();
            _eventPublisher.EntityUpdated(tag);
        }

        public async Task<Tag> FindByIdAsync(Guid tagId)
        {
            return await _tagRepository
                  .FirstOrDefaultAsync(model => model.Id == tagId);
        }

        public async Task<string> GenerateCodeForTagAsync()
        {
            var request = new TagSearchModel
            {
                PageSize = PageSize.All
            };
            var maxCode = await MaxCodeByRequestAsync(request, TagAggregateMemberModel.Code);

            if (maxCode == null)
                return (CodeConst.BeginNumber5Digit);
            return maxCode.ExtractNumeric();
        }

        public async Task<IList<Tag>> GetActiveAsync()
        {
            var request = new TagSearchModel
            {
                SortDirection = SortDirection.Asc,
                SortMember = SortMember.CreatedOn,
                PageSize = PageSize.Count100,
                PageIndex = 1,
                IsActive = true
            };
            return await GetByRequestAsync(request);
        }

        public async Task<IList<Tag>> GetByRequestAsync(TagSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return await QueryByRequest(model).ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        public async Task<IList<FineUploaderObject>> GetFileAsFineUploaderModelByIdAsync(Guid tagId)
        {
            return (await _tagRepository.AsNoTracking()
                    .Where(model => model.Id == tagId)
                    .Select(model => new
                    {
                        model.Id,
                        model.LogoFileName
                    }).ToListAsync())
                .Select(model => new FineUploaderObject
                {
                    id = model.Id.ToString(),
                    uuid = model.Id.ToString(),
                    name = model.LogoFileName,
                    thumbnailUrl = FileConst.ImagesWebPath.PlusWord(model.LogoFileName),
                    size = new FileInfo(HttpContext.Current.Server.MapPath(FileConst.ImagesWebPath.PlusWord(model.LogoFileName))).Length.ToString()
                }).ToList();
        }

        public async Task<string> MaxCodeByRequestAsync(TagSearchModel model, string aggregateMember)
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

        public IQueryable<Tag> QueryByRequest(TagSearchModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var tags = _tagRepository.AsNoTracking().AsQueryable();
            if (model.Term.HasValue())
                tags = tags.Where(m => m.Title.Contains(model.Term) || m.Description.Contains(model.Term));
            if (model.IsActive.HasValue)
                tags = tags.Where(m => m.IsActive == model.IsActive);
            if (string.IsNullOrEmpty(model.SortMember))
                model.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(model.SortDirection))
                model.SortDirection = SortDirection.Desc;
            tags = tags.OrderBy($"{model.SortMember} {model.SortDirection}");

            return tags;
        }

        public async Task SeedAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}