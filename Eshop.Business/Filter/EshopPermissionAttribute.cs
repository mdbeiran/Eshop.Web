using System.Web;
using System.Web.Mvc;
using Eshop.Business.Users;

namespace Eshop.Business.Filter
{
    public class EshopPermissionAttribute : AuthorizeAttribute
    {
        private string permissionName;

        public EshopPermissionAttribute(string permissionName)
        {
            this.permissionName = permissionName;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return PermissionChecker.PermissionCheck(permissionName);
        }
    }
}
