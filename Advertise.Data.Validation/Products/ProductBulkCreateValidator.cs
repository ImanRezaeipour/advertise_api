using Advertise.Core.Model.Products;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Products
{
    public class ProductBulkCreateValidator : BaseValidator<ProductBulkCreateModel>
    {
        public ProductBulkCreateValidator()
        {
            RuleFor(model => model.ProductBulks).SetCollectionValidator(new ProductBulkValidator());
        }
    }
}