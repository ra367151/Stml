using Stml.Infrastructure.Authorizations.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stml.Web.Startup.Permissions
{
    public class StmlPermissionProvider : IPermissionProvider<Permission>
    {
        public void Initialize(IPermissionProviderContext<Permission> context)
        {

            context.Manager.AddPermission(new Permission(PermissionGroupNames.Home, PermissionNames.VisitHome, "访问首页"));

            context.Manager.AddPermission(new Permission(PermissionGroupNames.Role, PermissionNames.VisitRolePage, "访问角色页面"));
            context.Manager.AddPermission(new Permission(PermissionGroupNames.Role, PermissionNames.RoleCreate, "添加角色"));
            context.Manager.AddPermission(new Permission(PermissionGroupNames.Role, PermissionNames.RoleEdit, "编辑角色"));
            context.Manager.AddPermission(new Permission(PermissionGroupNames.Role, PermissionNames.RoleDelete, "删除角色"));

            context.Manager.AddPermission(new Permission(PermissionGroupNames.User, PermissionNames.VisitUserPage, "访问用户页面"));
            context.Manager.AddPermission(new Permission(PermissionGroupNames.User, PermissionNames.UserCreate, "添加用户"));
            context.Manager.AddPermission(new Permission(PermissionGroupNames.User, PermissionNames.UserEdit, "编辑用户"));
            context.Manager.AddPermission(new Permission(PermissionGroupNames.User, PermissionNames.UserDelete, "删除用户"));

            context.Manager.AddPermission(new Permission(PermissionGroupNames.Setting, PermissionNames.VisitSettingPage, "访问配置页面"));
        }
    }
}
