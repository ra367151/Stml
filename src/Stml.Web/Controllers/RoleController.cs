using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
            , IMapper mapper
            , IRoleAppService roleAppService)
            : base(permissionPacker, permissionManager, mapper)
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
            return PartialView("_Edit", _mapper.Map<RoleEditViewModel>(role));
        }

        [HasPermission(PermissionNames.RoleCreate)]
        [Ajax(Http.Post)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Json(await _roleAppService.CreateRoleAsync(_mapper.Map<RoleCreateInput>(model)));
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
                return Json(await _roleAppService.EditRoleAsync(_mapper.Map<RoleEditInput>(model)));
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