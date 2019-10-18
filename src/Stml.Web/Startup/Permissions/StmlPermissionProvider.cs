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

            context.Manager.AddPermission(new Permission(PermissionGroupNames.Homes, PermissionNames.VisitHome, "访问首页"));

            context.Manager.AddPermission(new Permission(PermissionGroupNames.Roles, PermissionNames.VisitRoles, "访问角色页面"));
            context.Manager.AddPermission(new Permission(PermissionGroupNames.Roles, PermissionNames.RoleCreate, "添加角色"));
            context.Manager.AddPermission(new Permission(PermissionGroupNames.Roles, PermissionNames.RoleEdit, "编辑角色"));
            context.Manager.AddPermission(new Permission(PermissionGroupNames.Roles, PermissionNames.RoleDelete, "删除角色"));

            context.Manager.AddPermission(new Permission(PermissionGroupNames.Users, PermissionNames.VisitUsers, "访问用户页面"));
            context.Manager.AddPermission(new Permission(PermissionGroupNames.Users, PermissionNames.UserCreate, "添加用户"));
            context.Manager.AddPermission(new Permission(PermissionGroupNames.Users, PermissionNames.UserEdit, "编辑用户"));
            context.Manager.AddPermission(new Permission(PermissionGroupNames.Users, PermissionNames.UserDelete, "删除用户"));

            context.Manager.AddPermission(new Permission(PermissionGroupNames.Settings, PermissionNames.VisitSettings, "访问配置页面"));
        }
    }
}
