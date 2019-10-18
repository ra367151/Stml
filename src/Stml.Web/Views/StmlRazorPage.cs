using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Stml.Infrastructure.Authorizations.Permissions;

namespace Stml.Web.Views
{
    public abstract class StmlRazorPage<TModel> : RazorPage<TModel>
    {
        [RazorInject]
        public IPermissionChecker PermissionChecker { get; set; }

        protected virtual bool IsGranted(string permissionName)
        {
            return PermissionChecker.Check(User, permissionName);
        }
    }

}
