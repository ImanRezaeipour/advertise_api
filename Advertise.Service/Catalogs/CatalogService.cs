using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web;
using Advertise.Core.Common;
using Advertise.Core.Domain.Catalogs;
using Advertise.Core.Extensions;
using Advertise.Core.Managers.Event;
using Advertise.Core.Managers.ExportImport;
using Advertise.Core.Managers.ExportImport.Help;
using Advertise.Core.Managers.File;
using Advertise.Core.Managers.Html;
using Advertise.Core.Managers.WebContext;
using Advertise.Core.Model.Catalogs;
using Advertise.Core.Model.Common;
using Advertise.Core.Objects;
using Advertise.Core.Types;
using Advertise.Data.DbContexts;
using Advertise.Service.Categories;
using Advertise.Service.Keywords;
using Advertise.Service.Manufacturers;
using Advertise.Service.Products;
using Advertise.Service.Services.Keywords;
using Advertise.Service.Specifications;
using AutoMapper;
using OfficeOpenXml;

namespace Advertise.Service.Catalogs
{
    public class CatalogService : ICatalogService
    {
        #region Private Fields

        private readonly IDbSet<Catalog> _catalogRepository;
        private readonly IProductService _productService;
        private readonly ICatalogSpecificationService _catalogSpecificationService;
        private readonly ICategoryService _categoryService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebContextManager _webContextManager;
        private readonly IKeywordService _keywordService;
        private readonly IManufacturerService _manufacturerService;
        private readonly ISpecificationService _specificationService;
        private readonly ISpecificationOptionService _specificationOptionService;

        #endregion Private Fields

        #region Public Constructors

        public CatalogService(IUnitOfWork unitOfWork, IMapper mapper,IProductService productService, IEventPublisher eventPublisher, ICategoryService categoryService, IWebContextManager webContextManager, ICatalogSpecificationService catalogSpecificationService, IKeywordService keywordService, IManufacturerService manufacturerService, ISpecificationService specificationService, ISpecificationOptionService specificationOptionService)
        {
            _unitOfWork = unitOfWork;
            _catalogRepository = unitOfWork.Set<Catalog>();
            _mapper = mapper;
            _eventPublisher = eventPublisher;
            _categoryService = categoryService;
            _webContextManager = webContextManager;
            _productService = productService;
            _catalogSpecificationService = catalogSpecificationService;
            _keywordService = keywordService;
            _manufacturerService = manufacturerService;
            _specificationService = specificationService;
            _specificationOptionService = specificationOptionService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> CountByRequestAsync(CatalogSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).CountAsync();
        }

        public async Task CreateByViewModelAsync(CatalogCreateModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var catalog = _mapper.Map<Catalog>(viewModel);
            catalog.Description = viewModel.Description.ToSafeHtml();
            catalog.CreatedById = _webContextManager.CurrentUserId;
            catalog.Code = await _productService.GenerateCodeAsync();

            if (viewModel.Features != null)
            {
                var features = _mapper.Map<IList<CatalogFeature>>(viewModel.Features);
                catalog.Features.Clear();
                foreach (var catalogFeature in features)
                {
                    catalog.Features.Add(catalogFeature);
                }
            }

            if (viewModel.Images != null)
            {
                var images = _mapper.Map<IList<CatalogImage>>(viewModel.Images);
                catalog.Images.Clear();
                foreach (var catalogImage in images)
                {
                    catalog.Images.Add(catalogImage);
                }
            }

            var keywords = viewModel.CatalogKeywords;
            var catalogKeywords = new List<CatalogKeyword>();
            if (keywords != null)
            {
                foreach (var keyword in keywords)
                {
                    var catalogKeyword = new CatalogKeyword();
                    Guid isGuid;
                    Guid.TryParse(keyword, out isGuid);
                    if (isGuid != Guid.Empty)
                        catalogKeyword.KeywordId = keyword.ToGuid();
                    else
                        catalogKeyword.Title = keyword;

                    catalogKeywords.Add(catalogKeyword);
                }
                catalog.Keywords = catalogKeywords;
            }

            //  Catalog Specification
            if (viewModel.Specifications != null)
            {
                var catalogSpecifications = new List<CatalogSpecification>();
                foreach (var specification in viewModel.Specifications)
                {
                    if (specification.SpecificationOptionIdList != null)
                    {
                        foreach (var specificationOption in specification.SpecificationOptionIdList)
                        {
                            var catalogSpecification = new CatalogSpecification
                            {
                                SpecificationId = specification.Id,
                                SpecificationOptionId = specificationOption
                            };
                            catalogSpecifications.Add(catalogSpecification);
                        }
                    }
                    else if (specification.Value != null)
                    {
                        var catalogSpecification = new CatalogSpecification
                        {
                            SpecificationId = specification.Id,
                            Value = specification.Value
                        };
                        catalogSpecifications.Add(catalogSpecification);
                    }
                }
                catalog.Specifications = catalogSpecifications;
            }

            _catalogRepository.Add(catalog);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(catalog);
        }


        public async Task CreateByViewModelAsync(CatalogExportModel viewModel)
        {
            var catalog = _mapper.Map<Catalog>(viewModel);
             catalog.Code = await _productService.GenerateCodeAsync();
            catalog.CreatedById = _webContextManager.CurrentUserId;
            catalog.Features = new HashSet<CatalogFeature>();

            if (string.IsNullOrEmpty(viewModel.FeatureTitle1) == false)
                catalog.Features.Add(new CatalogFeature
                {
                    Title = viewModel.FeatureTitle1
                });

            if (string.IsNullOrEmpty(viewModel.FeatureTitle2) == false)
                catalog.Features.Add(new CatalogFeature
                {
                    Title = viewModel.FeatureTitle2
                });

            if (string.IsNullOrEmpty(viewModel.FeatureTitle3) == false)
                catalog.Features.Add(new CatalogFeature
                {
                    Title = viewModel.FeatureTitle3
                });

            if (string.IsNullOrEmpty(viewModel.FeatureTitle4) == false)
                catalog.Features.Add(new CatalogFeature
                {
                    Title = viewModel.FeatureTitle4
                });

            if (string.IsNullOrEmpty(viewModel.FeatureTitle5) == false)
                catalog.Features.Add(new CatalogFeature
                {
                    Title = viewModel.FeatureTitle5
                });

            if (viewModel.Specifications == null)
                catalog.Specifications.Clear();
            else
            {
                catalog.Specifications.Clear();
                catalog.Specifications = new HashSet<CatalogSpecification>();
                var specifications = _mapper.Map<IList<CatalogSpecification>>(viewModel.Specifications);
                foreach (var specification in specifications)
                {
                    catalog.Specifications.Add(specification);
                }
            }

            _catalogRepository.Add(catalog);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityInserted(catalog);
        }

        public async Task DeleteByIdAsync(Guid catalogId)
        {
            var catalog = await FindByIdAsync(catalogId);
            _catalogRepository.Remove(catalog);

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityDeleted(catalog);
        }

        public async Task EditByViewModelAsync(CatalogEditModel viewModel)
        {
            var catalog = await FindByIdAsync(viewModel.Id);
            _mapper.Map(viewModel, catalog);

            catalog.Description = viewModel.Description.ToSafeHtml();

            if (viewModel.Features != null)
            {
                var features = _mapper.Map<IList<CatalogFeature>>(viewModel.Features);
                catalog.Features.Clear();
                foreach (var catalogFeature in features)
                {
                    catalog.Features.Add(catalogFeature);
                }
            }
            else
            {
                catalog.Features.Clear();
            }

            if (viewModel.Images != null)
            {
                var images = _mapper.Map<IList<CatalogImage>>(viewModel.Images);
                catalog.Images.Clear();
                foreach (var catalogImage in images)
                {
                    catalog.Images.Add(catalogImage);
                }
            }
            else
            {
                catalog.Images.Clear();
            }

            var keywords = viewModel.CatalogKeywords;
            var catalogKeywords = new List<CatalogKeyword>();
            if (keywords != null)
            {
                foreach (var keyword in keywords)
                {
                    var catalogKeyword = new CatalogKeyword();
                    Guid isGuid;
                    Guid.TryParse(keyword, out isGuid);
                    if (isGuid != Guid.Empty)
                        catalogKeyword.KeywordId = keyword.ToGuid();
                    else
                        catalogKeyword.Title = keyword;

                    catalogKeywords.Add(catalogKeyword);
                }
                catalog.Keywords.Clear();
                foreach (var catalogKeyword in catalogKeywords)
                {
                    catalog.Keywords.Add(catalogKeyword);
                }
            }
            else
            {
                catalog.Keywords.Clear();
            }

            //  Catalog Specification
            if (viewModel.Specifications != null)
            {
                var catalogSpecifications = new List<CatalogSpecification>();
                foreach (var specification in viewModel.Specifications)
                {
                    if (specification.SpecificationOptionIdList != null)
                    {
                        foreach (var specificationOption in specification.SpecificationOptionIdList)
                        {
                            var catalogSpecification = new CatalogSpecification
                            {
                                SpecificationId = specification.Id,
                                SpecificationOptionId = specificationOption
                            };
                            catalogSpecifications.Add(catalogSpecification);
                        }
                    }
                    else if (specification.Value != null)
                    {
                        var catalogSpecification = new CatalogSpecification
                        {
                            SpecificationId = specification.Id,
                            Value = specification.Value
                        };
                        catalogSpecifications.Add(catalogSpecification);
                    }
                }
                catalog.Specifications.Clear();
                //catalog.Specifications = catalogSpecifications;
                foreach (var catalogSpecification in catalogSpecifications)
                {
                    catalog.Specifications.Add(catalogSpecification);
                }
            }
            else
            {
                catalog.Specifications.Clear();
            }

            await _unitOfWork.SaveAllChangesAsync();

            _eventPublisher.EntityUpdated(catalog);
        }

        public async Task<Catalog> FindByIdAsync(Guid catalogId)
        {
            return await _catalogRepository.SingleOrDefaultAsync(model => model.Id == catalogId);
        }

        public async Task<IList<Select2Object>> GetAsSelect2ObjectAsync()
        {
            var subCategories = await _categoryService.GetAllChildsByIdAsync(_categoryService.CurrentCategoryId);

            return _catalogRepository.AsNoTracking()
                .Where(model => subCategories.Contains(model.CategoryId.Value))
                .Select(model => new Select2Object
                {
                    id = model.Id,
                    text = model.Title,
                    related_id = model.CategoryId.Value
                })
                .ToList();
        }

        public async Task<IList<SelectList>> GetAsSelectListAsync()
        {
            var subCategories = await _categoryService.GetAllChildsByIdAsync(_categoryService.CurrentCategoryId);

            return _catalogRepository.AsNoTracking()
                .Where(model => subCategories.Contains(model.CategoryId.Value))
                .Select(model => new SelectList
                {
                    Text = model.Title,
                    Value = model.Id.ToString()
                })
                .ToList();
        }

        public async Task<IList<Catalog>> GetByRequestAsync(CatalogSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await QueryByRequest(request).ToPagedListAsync(request.PageIndex, request.PageSize);
        }

        public async Task<IList<FineUploaderObject>> GetFileAsFineUploaderModelByIdAsync(Guid catalogId)
        {
            return (await _catalogRepository.AsNoTracking()
                    .Where(model => model.Id == catalogId)
                    .Select(model => new
                    {
                        model.Id,
                        model.ImageFileName
                    }).ToListAsync())
                .Select(model => new FineUploaderObject
                {
                    id = model.Id.ToString(),
                    uuid = model.Id.ToString(),
                    name = model.ImageFileName,
                    thumbnailUrl = FileConst.ImagesCatalogWebPath.PlusWord(model.ImageFileName),
                    size = new FileInfo(HttpContext.Current.Server.MapPath(FileConst.ImagesCatalogWebPath.PlusWord(model.ImageFileName))).Length.ToString()
                }).ToList();
        }

        public async Task<IList<FineUploaderObject>> GetFilesAsFineUploaderModelByIdAsync(Guid catalogId)
        {
            return (await _catalogRepository.AsNoTracking()
                .Include(model => model.Images)
                    .Where(model => model.Id == catalogId)
                    .Select(model => model.Images)
                    .SingleOrDefaultAsync())
                .Select(model => new FineUploaderObject
                {
                    id = model.Id.ToString(),
                    uuid = model.Id.ToString(),
                    name = model.FileName,
                    thumbnailUrl = FileConst.ImagesCatalogWebPath.PlusWord(model.FileName),
                    size = new FileInfo(HttpContext.Current.Server.MapPath(FileConst.ImagesCatalogWebPath.PlusWord(model.FileName))).Length.ToString()
                }).ToList();
        }

        public IQueryable<Catalog> QueryByRequest(CatalogSearchModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var catalog = _catalogRepository.AsNoTracking().AsQueryable();

            if (request.CreatedById != null)
                catalog = catalog.Where(model => model.CreatedById == request.CreatedById);
            if (request.Term.HasValue())
                catalog = catalog.Where(model => model.Title.Contains(request.Term));

            if (string.IsNullOrEmpty(request.SortMember))
                request.SortMember = SortMember.CreatedOn;
            if (string.IsNullOrEmpty(request.SortDirection))
                request.SortDirection = SortDirection.Desc;

            catalog = catalog.OrderBy($"{request.SortMember} {request.SortDirection}");

            return catalog;
        }
        
        public async Task<byte[]> GetCatalogTemplateAsExcelAsync(Guid categoryId)
        {
            var defaultCatalog = new List<CatalogExportModel>
            {
                new CatalogExportModel
                {
                    Code = "100",
                }
            };

            return await ExportCatalogsToXlsxAsync(defaultCatalog, categoryId);
        }
        
        public async Task<byte[]> ExportCatalogsToXlsxAsync(IEnumerable<CatalogExportModel> catalogs, Guid categoryId)
        {
            var properties = new[]
            {
                new PropertyByName<CatalogExportModel>("Id", p => p.Id),
                new PropertyByName<CatalogExportModel>("Body", p => p.Body),
                new PropertyByName<CatalogExportModel>("Code", p => p.Code),
                new PropertyByName<CatalogExportModel>("Description", p => p.Description),
                new PropertyByName<CatalogExportModel>("FeatureTitle1", p => p.FeatureTitle1),
                new PropertyByName<CatalogExportModel>("FeatureTitle2", p => p.FeatureTitle2),
                new PropertyByName<CatalogExportModel>("FeatureTitle3", p => p.FeatureTitle3),
                new PropertyByName<CatalogExportModel>("FeatureTitle4", p => p.FeatureTitle4),
                new PropertyByName<CatalogExportModel>("FeatureTitle5", p => p.FeatureTitle5),
                new PropertyByName<CatalogExportModel>("ImageFileName1", p => p.ImageFileName1),
                new PropertyByName<CatalogExportModel>("ImageFileName2", p => p.ImageFileName2),
                new PropertyByName<CatalogExportModel>("ImageFileName3", p => p.ImageFileName3),
                new PropertyByName<CatalogExportModel>("ImageFileName4", p => p.ImageFileName4),
                new PropertyByName<CatalogExportModel>("ImageFileName5", p => p.ImageFileName5),
                new PropertyByName<CatalogExportModel>("KeywordId", p => "34BC5D0C-A987-EB33-EDFC-39E3761B5722")
                {
                    //DropDownElements = await _keywordService.GetAllActiveAsSelectListAsync(),
                    AllowBlank = true
                },
                new PropertyByName<CatalogExportModel>("ManufacturerId", p => "34BC5D0C-A987-EB33-EDFC-39E3761B5722")
                {
                    //DropDownElements = await _manufacturerService.GetAllAsSelectListAsync(),
                },
                new PropertyByName<CatalogExportModel>("MetaDescription", p => p.MetaDescription),
                new PropertyByName<CatalogExportModel>("MetaKeywords", p => p.MetaKeywords),
                new PropertyByName<CatalogExportModel>("MetaTitle", p => p.MetaTitle),
                new PropertyByName<CatalogExportModel>("ReviewBody", p => p.ReviewBody),
                new PropertyByName<CatalogExportModel>("Title", p => p.Title)
            };

            var extraProperties = new List<PropertyByName<CatalogExportModel>>();
            foreach (var specification in await _specificationService.GetByCategoryIdAsync(categoryId))
            {
                extraProperties.Add(new PropertyByName<CatalogExportModel>(specification.Title, p => "")
                {
                    //DropDownElements = await _specificationOptionService.GetAsSelectListBySpecificationIdAsync(specification.Id),
                    AllowBlank = true
                });
            }

            return new ExportManager().ExportToXlsx(properties.Concat(extraProperties).ToArray(), catalogs);
        }
        
        public async Task ImportCatalogsFromXlsxAsync(Stream stream, Guid categoryId)
        {
            using (var xlPackage = new ExcelPackage(stream))
            {
                // get the first worksheet in the workbook
                var worksheet = xlPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    throw new Exception("No worksheet found");

                //the columns
                var properties = new ImportManager().GetPropertiesByExcelCells<CatalogExportModel>(worksheet);

                var manager = new PropertyManager<CatalogExportModel>(properties);

                var iRow = 2;
                while (true)
                {
                    var allColumnsAreEmpty = manager.GetProperties
                        .Select(property => worksheet.Cells[iRow, property.PropertyOrderPosition])
                        .All(cell => string.IsNullOrEmpty(cell?.Value?.ToString()));

                    if (allColumnsAreEmpty)
                        break;

                    // read current row data
                    manager.ReadFromXlsx(worksheet, iRow);

                    bool isNew;
                    var catalogViewModel = new CatalogExportModel();
                    if (string.IsNullOrEmpty(manager.GetProperty("Id").StringValue))
                    {
                        isNew = true;
                    }
                    else
                    {
                        var catalog = await FindByIdAsync(Guid.Parse(manager.GetProperty("Id").StringValue));
                        if (catalog == null)
                        {
                            isNew = true;
                        }
                        else
                        {
                            isNew = false;
                            catalogViewModel = _mapper.Map<CatalogExportModel>(catalog);
                        }
                    }

                    foreach (var property in manager.GetProperties)
                    {
                        switch (property.PropertyName)
                        {
                            case "Title":
                                catalogViewModel.Title = property.StringValue;
                                break;

                            case "Body":
                                catalogViewModel.Body = property.StringValue;
                                break;

                            case "Code":
                                catalogViewModel.Code = property.StringValue;
                                break;

                            case "Description":
                                catalogViewModel.Description = property.StringValue;
                                break;

                            case "FeatureTitle1":
                                catalogViewModel.FeatureTitle1 = property.StringValue;
                                break;

                            case "FeatureTitle2":
                                catalogViewModel.FeatureTitle2 = property.StringValue;
                                break;

                            case "FeatureTitle3":
                                catalogViewModel.FeatureTitle3 = property.StringValue;
                                break;

                            case "FeatureTitle4":
                                catalogViewModel.FeatureTitle4 = property.StringValue;
                                break;

                            case "FeatureTitle5":
                                catalogViewModel.FeatureTitle5 = property.StringValue;
                                break;

                            case "ImageFileName1":
                                catalogViewModel.ImageFileName1 = property.StringValue;
                                break;

                            case "ImageFileName2":
                                catalogViewModel.ImageFileName2 = property.StringValue;
                                break;

                            case "ImageFileName3":
                                catalogViewModel.ImageFileName3 = property.StringValue;
                                break;

                            case "ImageFileName4":
                                catalogViewModel.ImageFileName4 = property.StringValue;
                                break;

                            case "ImageFileName5":
                                catalogViewModel.ImageFileName5 = property.StringValue;
                                break;

                            case "KeywordId":
                                catalogViewModel.KeywordId = property.StringValue;
                                break;

                            case "ManufacturerId":
                                catalogViewModel.ManufacturerId = property.GuidValue.Value;
                                break;

                            case "MetaDescription":
                                catalogViewModel.MetaDescription = property.StringValue;
                                break;

                            case "MetaKeywords":
                                catalogViewModel.MetaKeywords = property.StringValue;
                                break;

                            case "MetaTitle":
                                catalogViewModel.MetaTitle = property.StringValue;
                                break;

                            case "ReviewBody":
                                catalogViewModel.ReviewBody = property.StringValue;
                                break;
                        }
                    }

                    var catalogSpecification = new List<CatalogSpecification>();
                    foreach (var specification in await _specificationService.GetByCategoryIdAsync(categoryId))
                    {
                        if(string.IsNullOrEmpty(manager.GetProperty(specification.Title).StringValue))
                            continue;
                        catalogSpecification.Add(new CatalogSpecification
                        {
                            SpecificationId = specification.Id,
                            SpecificationOptionId = specification.Type == SpecificationType.String ? null : await _specificationOptionService.GetIdByTitleAsync(manager.GetProperty(specification.Title).StringValue, specification.Id),
                            Value = specification.Type == SpecificationType.String ? manager.GetProperty(specification.Title).StringValue : null
                        });
                    }

                    if (isNew)
                    {
                        //var createViewModel = _mapper.Map<CatalogCreateViewModel>(catalogViewModel);
                        catalogViewModel.Specifications = _mapper.Map<IList<CatalogSpecificationModel>>(catalogSpecification);
                        catalogViewModel.CategoryId = categoryId;
                        await CreateByViewModelAsync(catalogViewModel);
                    }
                    else
                    {
                        var editViewModel = _mapper.Map<CatalogEditModel>(catalogViewModel);
                        editViewModel.Specifications = _mapper.Map<IList<CatalogSpecificationModel>>(catalogSpecification);
                        editViewModel.CategoryId = categoryId;
                        await EditByViewModelAsync(editViewModel);
                    }

                    iRow++;
                }
            }
        }

        #endregion Public Methods
    }
}