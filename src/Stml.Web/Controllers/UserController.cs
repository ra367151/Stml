using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(IPermissionPacker permissionPacker
            , IPermissionManager<Permission> permissionManager
            , IMapper mapper
            , IUserAppService userAppService)
            : base(permissionPacker, permissionManager, mapper)
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
            var data = await _userAppService.GetUserPagedListAsync(search?.Trim(), offset, limit);
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
            return PartialView("_Create");
        }

        [HasPermission(PermissionNames.UserEdit)]
        [Ajax(Http.Get)]
        public async Task<IActionResult> EditPartial(Guid id)
        {
            var user = await _userAppService.FindUserAsync(id);
            if (user == null)
                throw new UserFriendlyException("用户不存在");
            return PartialView("_Edit", _mapper.Map<UserEditViewModel>(user));
        }

        [HasPermission(PermissionNames.UserCreate)]
        [Ajax(Http.Post)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Json(await _userAppService.CreateUserAsync(_mapper.Map<UserCreateInput>(model)));
            }
            return Json(ServiceResult.Fail(ModelState.Values.SelectMany(m => m.Errors).First().ErrorMessage));
        }

        [HasPermission(PermissionNames.UserEdit)]
        [Ajax(Http.Post)]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Json(await _userAppService.EditUserAsync(_mapper.Map<UserEditInput>(model)));
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