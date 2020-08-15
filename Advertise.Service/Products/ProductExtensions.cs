using System;
using System.Web;
using Advertise.Core.Infrastructure.DependencyManagement;
using Advertise.Core.Managers.WebContext;
using Advertise.Service.Services.Permissions;

namespace Advertise.Service.Products
{
    public static  class ProductExtensions
    {
        public static IProductService ProductService { get; set; } = ContainerManager.Container.GetInstance<IProductService>();
        public static IWebContextManager WebContextManager { get; set; } = ContainerManager.Container.GetInstance<IWebContextManager>();

        public static bool CanEditThisProduct(this Guid productId)
        {
            if (HttpContext.Current.User.IsInRole(PermissionConst.CanProductEdit))
                return true;
            
            var product = ProductService.GetByIdAsync(productId);
            if(HttpContext.Current.User.IsInRole(PermissionConst.CanProductMyEdit) && product.CreatedById == WebContextManager.CurrentUserId)
                return true;

            return false;
        }
    }
}
