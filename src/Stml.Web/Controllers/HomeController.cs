using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stml.Infrastructure.Authorizations.Permissions;
using Stml.Web.Models;

namespace Stml.Web.Controllers
{
    [Authorize]
    public class HomeController : StmlController
    {
        public HomeController(IPermissionPacker permissionPacker
            , IPermissionManager<Permission> permissionManager
            , IMapper mapper)
            : base(permissionPacker, permissionManager, mapper)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
