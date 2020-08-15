using Advertise.Core.Model.Products;
using Advertise.Data.Validation.Common;
using FluentValidation;

namespace Advertise.Data.Validation.Products
{
    public class ProductBulkEditValidator : BaseValidator<ProductBulkEditModel>
    {
        public ProductBulkEditValidator()
        {
            RuleFor(model => model.ProductBulks).SetCollectionValidator(new ProductBulkValidator());
        }
    }
}