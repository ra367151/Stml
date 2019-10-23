using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stml.Application.Dtos.Inputs;
using Stml.Application.Services;
using Stml.Infrastructure.Applications.Dto;
using Stml.Infrastructure.Applications.MVC.Http;
using Stml.Infrastructure.Authorizations.Permissions;
using Stml.Web.Startup.Permissions;
using static System.Net.WebRequestMethods;

namespace Stml.Web.Controllers
{
    [Authorize]
    public class UserController : StmlController
    {
        private readonly IUserAppService _userAppService;

        public UserController(IPermissionPacker permissionPacker
            , IPermissionManager<Permission> permissionManager
            , IUserAppService userAppService)
            : base(permissionPacker, permissionManager)
        {
            _userAppService = userAppService;
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
            var model = await _userAppService.FindUserEditModelAsync(id);
            return PartialView("Edit", model);
        }

        [HasPermission(PermissionNames.UserCreate)]
        [Ajax(Http.Post)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateInput model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userAppService.CreateUserAsync(model);
                return Json(result);
            }
            return Json(ServiceResult.Fail(ModelState.Values.SelectMany(m => m.Errors).First().ErrorMessage));
        }

        [HasPermission(PermissionNames.UserEdit)]
        [Ajax(Http.Post)]
        public async Task<IActionResult> Edit(UserEditInput model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userAppService.EditUserAsync(model);
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