using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Authorizations.Permissions
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string PermissionName)
            : base(PermissionName)
        {
        }
    }
}
