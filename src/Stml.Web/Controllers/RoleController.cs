using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stml.Application.Roles;
using Stml.Application.Roles.Dto;
using Stml.Infrastructure.Applications.Dto;
using Stml.Infrastructure.Applications.Exceptions;
using Stml.Infrastructure.Applications.MVC.Http;
using Stml.Infrastructure.Authorizations.Permissions;
using Stml.Web.Models.Roles;
using Stml.Web.Startup.Permissions;
using static System.Net.WebRequestMethods;

namespace Stml.Web.Controllers
{
    [Authorize]
    public class RoleController : StmlController
    {
        private readonly IRoleAppService _roleAppService;
        public RoleController(IPermissionPacker permissionPacker
            , IPermissionManager<Permission> permissionManager
            , IRoleAppService roleAppService)
            : base(permissionPacker, permissionManager)
        {
            _roleAppService = roleAppService;
        }

        [HasPermission(PermissionNames.VisitRolePage)]
        public IActionResult Index()
        {
            return View();
        }

        [HasPermission(PermissionNames.VisitRolePage)]
        [Ajax(Http.Get)]
        public async Task<IActionResult> List(string search, int offset = 0, int limit = 10)
        {
            var data = await _roleAppService.GetRolePagedListAsync(search, offset, limit);
            return Json(new
            {
                total = data.Total,
                rows = data.Rows
            });
        }

        [HasPermission(PermissionNames.RoleCreate)]
        [Ajax(Http.Get)]
        public IActionResult CreatePartial()
        {
            return PartialView("_Create");
        }

        [HasPermission(PermissionNames.RoleEdit)]
        [Ajax(Http.Get)]
        public async Task<IActionResult> EditPartial(Guid id)
        {
            var role = await _roleAppService.FindRoleAsync(id);
            if (role == null)
                throw new UserFriendlyException("角色不存在");
            return PartialView("_Edit", new RoleEditViewModel
            {
                Id = id,
                Name = role.Name,
                DisplayName = role.DisplayName,
                Permissions = _permissionManager.Permissions
                                        .SelectMany(x => x.Value)
                                        .OrderBy(x => x.Group)
                                        .Select(x => new PermissionCheckboxViewModel(
                                            x.Group,
                                            x.Name,
                                            x.DisplayName,
                                            role.Permissions.Any(p => p.Name == x.Name))
                                        ).ToList()
            });
        }

        [HasPermission(PermissionNames.RoleCreate)]
        [Ajax(Http.Post)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleAppService.CreateRoleAsync(new RoleCreateInput
                {
                    Name = model.Name,
                    DisplayName = model.DisplayName,
                    Permissions = model.Permissions.Where(x => x.Checked).Select(x => x.Name).ToArray()
                });
                return Json(result);
            }
            return Json(ServiceResult.Fail(ModelState.Values.SelectMany(m => m.Errors).First().ErrorMessage));
        }

        [HasPermission(PermissionNames.RoleEdit)]
        [Ajax(Http.Post)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleAppService.EditRoleAsync(new RoleEditInput
                {
                    Id = model.Id,
                    Name = model.Name,
                    DisplayName = model.DisplayName,
                    Permissions = model.Permissions.Where(x => x.Checked).Select(x => x.Name).ToArray()
                });
                return Json(result);
            }
            return Json(ServiceResult.Fail(ModelState.Values.SelectMany(m => m.Errors).First().ErrorMessage));
        }

        [HasPermission(PermissionNames.RoleDelete)]
        [Ajax(Http.Post)]
        public async Task Delete(Guid id)
        {
            await _roleAppService.DeleteRoleAsync(id);
        }
    }
}