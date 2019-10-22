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
using static System.Net.WebRequestMethods;

namespace Stml.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List(string search, int offset = 0, int limit = 10)
        {
            var data = await _userAppService.GetUserPagedListAsync(search, offset, limit);
            return Json(new
            {
                total = data.Total,
                rows = data.Rows
            });
        }

        [Ajax(Http.Get)]
        public IActionResult CreatePartial()
        {
            return PartialView("Create");
        }

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

        [Ajax(Http.Post)]
        public async Task Delete(Guid id)
        {
            await _userAppService.DeleteUserAsync(id);
        }
    }
}