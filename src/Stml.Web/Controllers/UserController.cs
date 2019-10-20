using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stml.Application.Services;

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

        //[HttpPost]
        public async Task<IActionResult> List(int limit, int offset)
        {
            var data = await _userAppService.GetUserPagedListAsync(string.Empty, 1, 10);
            return Json(new { total = data.Total, rows = data.Rows });
        }
    }
}