using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stml.Application.Accounts;
using Stml.Application.Accounts.Dto;
using Stml.Infrastructure.Authorizations.Permissions;
using Stml.Web.Models.Accounts;

namespace Stml.Web.Controllers
{
    public class AccountController : StmlController
    {
        private readonly IAccountAppService _accountAppService;

        public AccountController(IPermissionPacker permissionPacker
            , IPermissionManager<Permission> permissionManager
            , IAccountAppService accountAppService)
            : base(permissionPacker, permissionManager)
        {
            _accountAppService = accountAppService;
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
                var result = await _accountAppService.UserLoginAsync(new UserLoginInput(model.UserName, model.Password, model.RememberMe));
                if (result)
                {
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