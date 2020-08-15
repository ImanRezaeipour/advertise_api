using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Helpers;
using Advertise.Core.Model.Tags;
using Advertise.Core.Types;
using Advertise.Service.Common;
using Advertise.Service.Tags;
using AutoMapper;

namespace Advertise.Service.Factory.Tags
{
    public class TagFactory : ITagFactory
    {
        #region Private Fields

        private readonly ICommonService _commonService;
        private readonly IListService _listService;
        private readonly IMapper _mapper;
        private readonly ITagService _tagService;

        #endregion Private Fields

        #region Public Constructors

        public TagFactory(ICommonService commonService, IMapper mapper, ITagService tagService, IListService listService)
        {
            _commonService = commonService;
            _mapper = mapper;
            _tagService = tagService;
            _listService = listService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<TagCreateModel> PrepareCreateModelAsync(TagCreateModel modelPrepare= null)
        {
            var viewModel = modelPrepare?? new TagCreateModel();
            viewModel.ColorTypeList = EnumHelper.CastToSelectListItems<ColorType>();

            return viewModel;
        }

        public async Task<TagEditModel> PrepareEditModelAsync(Guid tagId, TagEditModel modelPrepare = null)
        {
            if (tagId == null)
                throw new ArgumentNullException(nameof(tagId));

            var tag = await _tagService.FindByIdAsync(tagId);
            var viewModel = modelPrepare ?? _mapper.Map<TagEditModel>(tag);
            viewModel.ColorTypeList = EnumHelper.CastToSelectListItems<ColorType>();

            return viewModel;
        }

        public async Task<TagListModel> PrepareListModelAsync(TagSearchModel model, bool isCurrentUser = false, Guid? userId = null)
        {
            model.CreatedById = await _commonService.GetUserIdAsync(isCurrentUser, userId);
            model.TotalCount = await _tagService.CountByRequestAsync(model);
            var tag = await _tagService.GetByRequestAsync(model);
            var tagViewModel = _mapper.Map<IList<TagModel>>(tag);
            var viewModel = new TagListModel
            {
                SearchModel = model,
                Tags = tagViewModel,
                SortDirectionList = await _listService.GetSortDirectionListAsync(),
                ActiveList = EnumHelper.CastToSelectListItems<ActiveType>(),
                PageSizeList = await _listService.GetPageSizeListAsync()
            };

            return viewModel;
        }

        #endregion Public Methods
    }
}