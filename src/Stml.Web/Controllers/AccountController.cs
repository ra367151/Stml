using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stml.Application.Dtos.Inputs;
using Stml.Application.Services;

namespace Stml.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountAppService _accountAppService;

        public AccountController(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserRegisterInput model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountAppService.UserRegisterAsync(model);
                if (result.IsSuccess)
                {
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLoginInput model, string returnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                var result = await _accountAppService.UserLoginAsync(model);
                if (result)
                {
                    return Redirect(returnUrl);
                }
                ModelState.AddModelError("", "用户名或密码不正确");
            }
            return View(model);
        }
    }
}