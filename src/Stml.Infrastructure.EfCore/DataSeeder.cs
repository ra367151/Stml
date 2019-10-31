using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Stml.Domain.Authorizations;
using Stml.Infrastructure.Authorizations.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stml.EntityFrameworkCore
{
    public static class DataSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            SeedAdministratorRole(roleManager);
            SeedAdministratorUser(userManager, roleManager);
        }

        private static void SeedAdministratorRole(RoleManager<Role> roleManager)
        {
            if (!roleManager.RoleExistsAsync(RoleConstants.DefaultAdminRoleName).Result)
            {
                roleManager.CreateAsync(Role.CreateAdministratorRole()).Wait();
            }
        }

        private static void SeedAdministratorUser(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (userManager.FindByNameAsync(UserConstants.DefaultAdminUserName).Result == null)
            {
                var role = roleManager.FindByNameAsync(RoleConstants.DefaultAdminRoleName).Result;
                var user = User.CreateAdministrator().SetUserToRoles(role);
                userManager.CreateAsync(user, UserConstants.DefaultUserPassword).Wait();
            }
        }
    }
}
