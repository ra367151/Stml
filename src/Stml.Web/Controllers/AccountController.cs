using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stml.Application.Accounts;
using Stml.Application.Accounts.Dto;
using Stml.Infrastructure.Authorizations.Permissions;
using Stml.Web.Models.Accounts;

namespace Stml.Web.Controllers
{
    public class AccountController : StmlController
    {
        private readonly IAccountAppService _accountAppService;
        private readonly ILogger<AccountController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IPermissionPacker permissionPacker
            , IPermissionManager<Permission> permissionManager
            , IMapper mapper
            , IAccountAppService accountAppService
            , ILogger<AccountController> logger
            , IHttpContextAccessor httpContextAccessor)
            : base(permissionPacker, permissionManager, mapper)
        {
            _accountAppService = accountAppService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLoginViewModel model, string returnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                var result = await _accountAppService.UserLoginAsync(_mapper.Map<UserLoginInput>(model));
                if (result)
                {
                    _logger.LogInformation($"User [{model.UserName}] login with ip-address: {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()}");
                    return Redirect(returnUrl);
                }
                ModelState.AddModelError("", "用户名或密码不正确");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            await _accountAppService.UserLogoutAsync();
            return Redirect("/");
        }
    }
}