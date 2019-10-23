using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Stml.Domain.Roles;
using Stml.Domain.Users;
using Stml.Infrastructure.Authorizations.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stml.Infrastructure.Datas
{
    public static class DataSeeder
    {
        private static readonly string SuperAdminUserName = "admin";
        private static readonly string SuperAdminUserEmail = "weiwxg@126.com";
        private static readonly string SuperAdminUserPassword = "123qwe";

        public static void Seed(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            SeedSuperAdminRole(roleManager);
            SeedSuperAdminUser(userManager);
        }

        private static void SeedSuperAdminRole(RoleManager<Role> roleManager)
        {
            if (!roleManager.RoleExistsAsync(PermissionConstants.SuperAdminRoleName).Result)
            {
                roleManager.CreateAsync(new Role { Name = PermissionConstants.SuperAdminRoleName }).Wait();
            }
        }

        private static void SeedSuperAdminUser(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync(SuperAdminUserName).Result == null)
            {
                var user = new User { UserName = SuperAdminUserName, Email = SuperAdminUserEmail };
                var identityResult = userManager.CreateAsync(user, SuperAdminUserPassword).Result;
                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, PermissionConstants.SuperAdminRoleName).Wait();
                }
            }
        }
    }
}
