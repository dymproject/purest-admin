// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PurestAdmin.Web.Entry.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Template()
        {
            return View();
        }
    }
}