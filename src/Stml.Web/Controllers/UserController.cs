using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stml.Application.Roles;
using Stml.Application.Services;
using Stml.Application.Users.Dto;
using Stml.Infrastructure.Applications.Dto;
using Stml.Infrastructure.Applications.Exceptions;
using Stml.Infrastructure.Applications.MVC.Http;
using Stml.Infrastructure.Authorizations.Permissions;
using Stml.Web.Models.Users;
using Stml.Web.Startup.Permissions;
using static System.Net.WebRequestMethods;

namespace Stml.Web.Controllers
{
    [Authorize]
    public class UserController : StmlController
    {
        private readonly IUserAppService _userAppService;
        private readonly IRoleAppService _roleAppService;

        public UserController(IPermissionPacker permissionPacker
            , IPermissionManager<Permission> permissionManager
            , IUserAppService userAppService
            , IRoleAppService roleAppService)
            : base(permissionPacker, permissionManager)
        {
            _userAppService = userAppService;
            _roleAppService = roleAppService;
        }

        [HasPermission(PermissionNames.VisitUserPage)]
        public IActionResult Index()
        {
            return View();
        }

        [HasPermission(PermissionNames.VisitUserPage)]
        [Ajax(Http.Get)]
        public async Task<IActionResult> List(string search, int offset = 0, int limit = 10)
        {
            var data = await _userAppService.GetUserPagedListAsync(search, offset, limit);
            return Json(new
            {
                total = data.Total,
                rows = data.Rows
            });
        }

        [HasPermission(PermissionNames.UserCreate)]
        [Ajax(Http.Get)]
        public IActionResult CreatePartial()
        {
            return PartialView("Create");
        }

        [HasPermission(PermissionNames.UserEdit)]
        [Ajax(Http.Get)]
        public async Task<IActionResult> EditPartial(Guid id)
        {
            var user = await _userAppService.FindUserAsync(id);
            if (user == null)
                throw new UserFriendlyException("用户不存在.");
            var roles = await _roleAppService.GetRolesAsync();
            return PartialView("Edit", new UserEditViewModel
            {
                Id = id,
                UserName = user.UserName,
                Email = user.Email,
                IsActive = user.IsActive,
                Roles = roles.Select(x => new RoleCheckboxViewModel(x.Name, x.DisplayName, user.Roles.Contains(x.Name))).ToList()
            });
        }

        [HasPermission(PermissionNames.UserCreate)]
        [Ajax(Http.Post)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userAppService.CreateUserAsync(new UserCreateInput
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                    IsActive = model.IsActive,
                    Roles = model.Roles.Where(x => x.Checked).Select(x => x.Name).ToArray()
                });
                return Json(result);
            }
            return Json(ServiceResult.Fail(ModelState.Values.SelectMany(m => m.Errors).First().ErrorMessage));
        }

        [HasPermission(PermissionNames.UserEdit)]
        [Ajax(Http.Post)]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userAppService.EditUserAsync(new UserEditInput
                {
                    Id = model.Id,
                    UserName = model.UserName,
                    Email = model.Email,
                    IsActive = model.IsActive,
                    Roles = model.Roles.Where(r => r.Checked).Select(r => r.Name).ToArray()
                });
                return Json(result);
            }
            return Json(ServiceResult.Fail(ModelState.Values.SelectMany(m => m.Errors).First().ErrorMessage));
        }

        [HasPermission(PermissionNames.UserDelete)]
        [Ajax(Http.Post)]
        public async Task Delete(Guid id)
        {
            await _userAppService.DeleteUserAsync(id);
        }
    }
}